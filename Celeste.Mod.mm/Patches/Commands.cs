﻿#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

using Celeste.Mod;
using Microsoft.Xna.Framework.Input;
using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using System.IO;
using FMOD.Studio;
using Monocle;
using Celeste.Mod.Meta;

namespace Celeste {
    static class patch_Commands {

        [MonoModReplace]
        [Command("capture", "capture the last ~200 frames of player movement to a file")]
        private static void CmdCapture(string filename) {
            Player player = Engine.Scene.Tracker.GetEntity<Player>();
            if (player != null) {
                PlaybackData.Export(player.ChaserStates, Path.Combine(Everest.Content.PathContentOrig, "Tutorials", filename + ".bin"));
            }
        }

        [MonoModReplace]
        [ProxyFileCalls]
        [Command("playback", "play back the file name")]
        private static void CmdPlayback(string filename) {
            filename = Path.Combine(Everest.Content.PathContentOrig, "Tutorials", filename + ".bin");
            if (File.Exists(filename)) {
                Engine.Scene = new PreviewRecording(filename);
            } else {
                Engine.Commands.Log("FILE NOT FOUND");
            }
        }

    }
}
