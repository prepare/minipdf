#region PDFsharp - A .NET library for processing PDF
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//
// Copyright (c) 2005-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections;
using System.ComponentModel;
#if GDI
using System.Drawing;
using System.Data;
using System.Windows.Forms;
#endif
#if Wpf
using System.Windows.Media;
#endif

namespace PdfSharp.Forms
{
    /// <summary>
    /// Implements the control that previews the page.
    /// </summary>
    class PagePreviewCanvas : System.Windows.Forms.Control
    {
        public PagePreviewCanvas(PagePreview preview)
        {
            this.preview = preview;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        }
        PagePreview preview;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.preview.showPage)
                return;

            Graphics gfx = e.Graphics;
            bool zoomChanged;
            this.preview.CalculatePreviewDimension(out zoomChanged);
            this.preview.RenderPage(gfx);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!this.preview.showPage)
            {
                e.Graphics.Clear(this.preview.desktopColor);
                return;
            }
            bool zoomChanged;
            this.preview.CalculatePreviewDimension(out zoomChanged);
            this.preview.PaintBackground(e.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
        }
    }
}