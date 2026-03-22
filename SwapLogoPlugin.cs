using System.IO;
using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwapLogo;

[BepInAutoPlugin(id: "io.github.carrieforle.swaplogo")]
public partial class SwapLogoPlugin : BaseUnityPlugin
{
    private void Awake()
    {
        SceneManager.activeSceneChanged += (fromScene, toScene) =>
        {
            if (toScene.name != "Pre_Menu_Intro")
            {
                return;
            }

            var logo = GameObject.Find("StartManager/1 - Team Cherry Logo");
            if (logo == null || !logo.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                return;
            }

            var res = LoadSpriteFromFile(
                [ "logo.png", "logo.jpg", "logo.jpeg" ]
            );
            if (res is null)
            {
                return;
            }
            
            spriteRenderer.sprite = res.Value.Item1;
            Logger.LogInfo($"Successfully swapped logo with \"{res.Value.Item2}\"");
        };
    }

    /// <summary>
    /// Load sprite from filepath.
    /// </summary>
    /// <returns>The first successfully loaded sprite</returns>
    private (Sprite, string)? LoadSpriteFromFile(params string[] filePaths)
    {
        foreach (string filePath in filePaths)
        {
            string fullPath = Path.Combine(Path.GetDirectoryName(Info.Location), filePath);
            Sprite? sprite = null;
            try
            {
                var payload = File.ReadAllBytes(fullPath);
                var texture = new Texture2D(0, 0);
                if (texture.LoadImage(payload))
                {
                    var rect = new Rect(0, 0, texture.width, texture.height);
                    var pivot = new Vector2(.5f, .5f);
                    sprite = Sprite.Create(texture, rect, pivot);
                }
            }
            catch
            {

            }

            if (sprite == null)
            {
                Logger.LogInfo($"Failed to load sprite frorm \"{fullPath}\"");
                continue;
            }

            return (sprite, filePath);
        }

        return null;
    }
}
