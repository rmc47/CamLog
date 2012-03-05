using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    public class LayoutAvery7162 : IPageLayout
    {
        public string Name
        {
            get { return "Avery 7162"; }
        }

        public int QsoPerLabel
        {
            get { return 4; }
        }

        public int Columns
        {
            get { return 2; }
        }

        public double LabelHeight
        {
            get { return 33.9; }
        }

        public double PageVerticalMargin
        {
            get { return 13; }
        }

        public double PageHorizontalMargin
        {
            get { return 5; }
        }

        public double LabelPaddingHorizontal
        {
            get { return 5; }
        }

        public float[] ColumnRelativeWidths
        {
            get { return new[] { 1.6f, 1, 1, 0.8f, 1.2f }; }
        }
    }
}
