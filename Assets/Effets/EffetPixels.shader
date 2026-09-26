/// WARNING FAIT PAR CLAUDE ///
// Déso mais c trop chiant de faire un shader je suis pas tech-art

// Effet d'écran n°2 : la pixelisation du Figma (réglages du Figma par défaut).
// L'image est découpée en petites cellules hexagonales : chaque cellule prend une seule couleur
// (un mélange de ses voisines), avec un nombre de nuances réduit.
// Il est appliqué à tout l'écran par le "Renderer2D" (Assets/Settings/Renderer2D.asset > Renderer Features).
Shader "Pupuce/EffetPixels"
{
    Properties
    {
        _TaillePixel ("Taille des cellules (pixels)", Range(1, 32)) = 5
        _Etirement ("Étirement des cellules (0 = 1 pixel d'épaisseur)", Range(0, 20)) = 0
        _Angle ("Angle des cellules (degrés)", Range(0, 360)) = 90
        _Nuances ("Nuances par couleur", Range(2, 16)) = 15
        _Melange ("Mélange avec les voisines", Range(0, 1)) = 0.8
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off ZTest Always Blend Off Cull Off

        Pass
        {
            Name "Pixels"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // Donne la fonction Vert et la texture _BlitTexture (= l'image du jeu avant l'effet)
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _TaillePixel;
            float _Etirement;
            float _Angle;
            float _Nuances;
            float _Melange;

            // Lit une couleur de l'image du jeu, convertie en "couleurs d'écran" (sRGB) comme dans Figma
            float3 Lire(float2 uv)
            {
                float3 c = SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_PointClamp, saturate(uv), 0).rgb;
                #if !defined(UNITY_COLORSPACE_GAMMA)
                c = LinearToSRGB(c);
                #endif
                return c;
            }

            // Centre de la cellule hexagonale qui contient le point p (en pixels)
            float2 CentreDeLaCellule(float2 p, float2 taille)
            {
                float rayon = taille.x * 0.5;
                float ecrasement = taille.y / taille.x;
                float nx = p.x;
                float ny = p.y / ecrasement;
                float fq = nx * 0.6666667 / rayon;
                float fr = (-nx * 0.3333333 + ny * 0.5773503) / rayon;
                float fs = -fq - fr;
                float rq = floor(fq + 0.5);
                float rr = floor(fr + 0.5);
                float rs = floor(fs + 0.5);
                float dq = abs(rq - fq);
                float dr = abs(rr - fr);
                float ds = abs(rs - fs);
                if (dq > dr && dq > ds) rq = -rr - rs;
                else if (dr > ds) rr = -rq - rs;
                return float2(1.5 * rayon * rq, 1.7320508 * rayon * (rr + rq * 0.5) * ecrasement);
            }

            float3 CouleurDeLaCellule(float2 pixel, float2 dims, float echelle)
            {
                float2 taille = max(float2(_TaillePixel, _TaillePixel * _Etirement) * echelle, 1.0);

                // On tourne la grille de cellules autour du centre de l'écran
                float2 centre = dims * 0.5;
                float angle = radians(_Angle);
                float c = cos(angle);
                float s = sin(angle);
                float2 rel = pixel - centre;
                float2 local = float2(rel.x * c + rel.y * s, -rel.x * s + rel.y * c);
                float2 cellule = CentreDeLaCellule(local, taille);
                float2 position = centre + float2(cellule.x * c - cellule.y * s, cellule.x * s + cellule.y * c);
                float2 uv = saturate(position / dims);

                // Mélange avec les 8 voisines
                float3 couleur = Lire(uv);
                float2 pas = taille * _Melange * 0.5 / dims;
                float3 somme = couleur;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i != 0 || j != 0) somme += Lire(uv + float2(i, j) * pas);
                    }
                }
                couleur = lerp(couleur, somme / 9.0, _Melange);

                // Moins de nuances
                float etapes = max(floor(_Nuances + 0.5), 2.0) - 1.0;
                return floor(saturate(couleur) * etapes + 0.5) / etapes;
            }

            float4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                float2 dims = _ScreenParams.xy;
                float echelle = dims.y / 1080.0; // le Figma fait 1080 pixels de haut
                float2 pixel = input.texcoord * dims;

                // 4 mesures un peu décalées puis la moyenne : les bords des cellules sont plus doux
                float d = 0.25;
                float3 couleur = CouleurDeLaCellule(pixel + float2(-d, -d), dims, echelle)
                               + CouleurDeLaCellule(pixel + float2( d, -d), dims, echelle)
                               + CouleurDeLaCellule(pixel + float2(-d,  d), dims, echelle)
                               + CouleurDeLaCellule(pixel + float2( d,  d), dims, echelle);
                couleur *= 0.25;

                #if !defined(UNITY_COLORSPACE_GAMMA)
                couleur = SRGBToLinear(couleur);
                #endif
                return float4(couleur, 1.0);
            }
            ENDHLSL
        }
    }
}
