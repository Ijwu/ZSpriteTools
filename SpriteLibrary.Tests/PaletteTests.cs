﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Xunit;

namespace SpriteLibrary.Tests
{
    public class PaletteTests
    {
        [Fact]
        public void should_build_palette_from_raw_data()
        {
            var file = File.ReadAllBytes("orb.old.spr");
            var pal15 = new byte[30];
            Array.Copy(file, 0x7000, pal15, 0, 30);

            var pal = new Palette();
            pal.SetRawPalette(pal15);

            Assert.Equal(Color.FromArgb(0, 0, 0), pal[0]);
            Assert.Equal(Color.FromArgb(248, 248, 248), pal[1]);
            Assert.Equal(Color.FromArgb(240, 216, 64), pal[2]);
            Assert.Equal(Color.FromArgb(192, 128, 240), pal[15]);
        }

        [Fact]
        public void should_build_palette_from_raw_array()
        {
            var palbytes = new byte[] { 0x00, 0x00,
                                        0xFF, 0x7F,
                                        0xFF, 0x00,
                                        0x00, 0xFF };

            var pal = new Palette();
            pal.SetRawPalette(palbytes);

            Assert.Equal(Color.FromArgb(0, 0, 0), pal[0]);
            Assert.Equal(Color.FromArgb(248, 248, 248), pal[1]);
            Assert.Equal(Color.FromArgb(248, 56, 0), pal[2]);
            Assert.Equal(Color.FromArgb(0, 192, 248), pal[3]);
        }
    }
}
