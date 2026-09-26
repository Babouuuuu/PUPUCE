/// WARNING FAIT PAR CLAUDE ///
// Déso mais c trop chiant de faire un shader je suis pas tech-art

// Effet d'écran n°1 : le vieux moniteur cathodique (CRT) du Figma (réglages du Figma par défaut).
// Il ajoute : les couleurs qui bavent vers les bords (aberration chromatique), les lignes de balayage,
// la grille de phosphores rouge/vert/bleu et un peu de neige.
// Il est appliqué à tout l'écran par le "Renderer2D" (Assets/Settings/Renderer2D.asset > Renderer Features).
Shader "Pupuce/EffetCRT"
{
    Properties
    {
        _Aberration ("Couleurs qui bavent (pixels)", Range(0, 12)) = 3
        _Balayage ("Lignes de balayage", Range(0, 1)) = 0.15
        _TailleBalayage ("Taille des lignes (pixels)", Range(1, 32)) = 3
        _Grille ("Grille rouge/vert/bleu", Range(0, 1)) = 0.5
        _TailleGrille ("Taille de la grille (pixels)", Range(2, 12)) = 6
        _Neige ("Neige", Range(0, 1)) = 0.15
        _TailleNeige ("Taille de la neige (pixels)", Range(1, 8)) = 1.5
        _Tremblement ("Tremblement des lignes", Range(0, 1)) = 0.03
        _Vitesse ("Vitesse d'animation (0 = image fixe, comme le Figma)", Range(0, 3)) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off ZTest Always Blend Off Cull Off

        Pass
        {
            Name "CRT"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // Donne la fonction Vert et la texture _BlitTexture (= l'image du jeu avant l'effet)
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _Aberration;
            float _Balayage;
            float _TailleBalayage;
            float _Grille;
            float _TailleGrille;
            float _Neige;
            float _TailleNeige;
            float _Tremblement;
            float _Vitesse;

            // Nombres pseudo-aléatoires (toujours le même résultat pour le même point)
            float Hasard2(float2 p)
            {
                float3 h = frac(float3(p.x, p.y, p.x) * 0.1031);
                h += dot(h, h.yzx + 33.33);
                return frac((h.x + h.y) * h.z);
            }

            float Hasard3(float3 p)
            {
                float3 h = frac(p * float3(0.1031, 0.1030, 0.0973));
                h += dot(h, h.yzx + 33.33);
                return frac((h.x + h.y) * h.z);
            }

            // Lit une couleur de l'image du jeu, convertie en "couleurs d'écran" (sRGB) comme dans Figma
            float4 Lire(float2 uv)
            {
                float4 c = SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, saturate(uv), 0);
                #if !defined(UNITY_COLORSPACE_GAMMA)
                c.rgb = LinearToSRGB(c.rgb);
                #endif
                return c;
            }

            // Profil d'une bande de phosphore : 3 bandes décalées pour le rouge, le vert et le bleu
            float3 ProfilDeLaGrille(float3 angle, float pas)
            {
                float lissage1 = smoothstep(2.0, 3.0, pas);
                float lissage2 = smoothstep(2.0, 3.0, pas * 0.5);
                return 0.375 + 0.5 * lissage1 * cos(angle) + 0.125 * lissage2 * cos(angle * 2.0);
            }

            float4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                float2 uv = input.texcoord;
                float2 dims = _ScreenParams.xy;
                float2 texel = 1.0 / dims;
                float echelle = dims.y / 1080.0; // le Figma fait 1080 pixels de haut
                float t = fmod(_Time.y * _Vitesse, 1024.0);

                float tailleBalayage = max(_TailleBalayage * echelle, 1.0);
                float tailleGrille = clamp(_TailleGrille * echelle, 2.0, 24.0);
                float tailleNeige = max(_TailleNeige * echelle, 1.0);

                // 1. Chaque ligne tremble un tout petit peu horizontalement
                float ligne = floor(uv.y * dims.y / tailleBalayage);
                float hasardLigne = Hasard2(float2(ligne, floor(t * 24.0))) - 0.5;
                float derive = sin(t * 1.7 + uv.y * 9.0) * 0.5;
                float2 p = uv;
                p.x += _Tremblement * (hasardLigne * 0.9 + derive * 0.4) * texel.x * 6.0;

                // 2. Aberration chromatique : rouge, vert et bleu sont lus à des endroits un peu différents,
                //    de plus en plus écartés en s'éloignant du centre de l'écran
                float2 aspect = float2(dims.x / dims.y, 1.0);
                aspect /= length(aspect) / sqrt(2.0);
                float2 depuisLeCentre = (uv * 2.0 - 1.0) * aspect;
                float rayon = length(depuisLeCentre);
                float2 direction = rayon > 0.0001 ? depuisLeCentre / rayon : float2(1.0, 0.0);
                float eloignement = saturate(rayon / sqrt(2.0));
                float2 decalage = direction * _Aberration * echelle * (0.72 + 0.48 * eloignement) * texel;
                float3 couleur = float3(Lire(p + decalage).r, Lire(p).g, Lire(p - decalage).b);

                // 3. Lignes de balayage horizontales
                float force = _Balayage * lerp(0.6, 1.0, smoothstep(1.0, 2.5, tailleBalayage));
                float y = p.y * dims.y + t * 6.0;
                float balayage = (1.0 - force * 0.5 * (1.0 - cos(TWO_PI * y / tailleBalayage))) / (1.0 - force * 0.5);
                couleur *= balayage;

                // 4. Grille de phosphores rouge / vert / bleu (bandes verticales fixées à l'écran)
                float angle = uv.x * dims.x * (TWO_PI / tailleGrille);
                float tiers = TWO_PI / 3.0;
                float3 profil = ProfilDeLaGrille(float3(angle, angle - tiers, angle + tiers), tailleGrille);
                float3 gain = (0.03 + 1.8 * profil) / (0.03 + 1.8 * 0.375);
                couleur *= lerp(1.0, gain, _Grille);

                // 5. Neige (bruit)
                float image = floor(t * 24.0);
                float2 grain = floor(uv * dims / tailleNeige);
                float blanc = Hasard3(float3(grain, image));
                float souffle = Hasard2(float2(floor(uv.y * dims.y / tailleNeige), image * 1.7)) - 0.5;
                float neige = saturate(blanc + souffle * 0.35);
                couleur = lerp(couleur, neige, saturate(_Neige) * 0.55);
                couleur *= 1.0 + (blanc - 0.5) * _Neige * 0.25;

                // 6. Les couleurs trop claires sont adoucies au lieu d'être coupées net
                float3 exces = max(couleur - 0.8, 0.0);
                float3 adoucie = 0.8 + 0.2 * (1.0 - exp(-exces / 0.2));
                couleur = lerp(couleur, adoucie, step(0.8, couleur));

                couleur = saturate(couleur);
                #if !defined(UNITY_COLORSPACE_GAMMA)
                couleur = SRGBToLinear(couleur);
                #endif
                return float4(couleur, 1.0);
            }
            ENDHLSL
        }
    }
}
