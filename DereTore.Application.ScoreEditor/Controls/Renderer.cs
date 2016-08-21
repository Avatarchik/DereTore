﻿using System;
using System.Collections.Generic;
using System.Drawing;
using DereTore.Application.ScoreEditor.Model;

namespace DereTore.Application.ScoreEditor.Controls {
    public sealed class Renderer {

        static Renderer() {
            InstanceSyncObject = new object();
        }

        public bool IsRendering {
            get {
                lock (_renderingSyncObject) {
                    return _isRendering;
                }
            }
            private set {
                lock (_renderingSyncObject) {
                    _isRendering = value;
                }
            }
        }

        public static Renderer Instance {
            get {
                lock (InstanceSyncObject) {
                    return _instance ?? (_instance = new Renderer());
                }
            }
        }

        public void RenderFrame(Graphics graphics, Size clientSize, TimeSpan timeSpan, List<Note> notes) {
            IsRendering = true;
            RenderHelper.DrawCeilingLine(graphics, clientSize);
            RenderHelper.DrawAvatars(graphics, clientSize);
            var now = (float)timeSpan.TotalSeconds;
            int startIndex, endIndex;
            //GetVisibleNotes(now, scores, out startIndex, out endIndex);
            startIndex = 0;
            endIndex = notes.Count - 1;
            RenderHelper.DrawNotes(graphics, clientSize, now, notes, startIndex, endIndex);
            IsRendering = false;
        }

        private Renderer() {
            _renderingSyncObject = new object();
        }

        private readonly object _renderingSyncObject;
        private static readonly object InstanceSyncObject;
        private static Renderer _instance;
        private bool _isRendering;

    }
}