using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    public class LayoutAvery7160 : IPageLayout
    {
        public string Name
        {
            get { return "Avery 7160"; }
        }

        public int QsoPerLabel
        {
            get { return 4; }
        }

        public int Columns
        {
            get { return 3; }
        }

        public double LabelHeight
        {
            get { return 38.1; }
        }

        public double PageVerticalMargin
        {
            get { return 13; }
        }

        public double PageHorizontalMargin
        {
            get { return 8; }
        }

        public double LabelPaddingHorizontal
        {
            get { return 5; }
        }

        public float[] ColumnRelativeWidths
        {
            get { return new[] { 1.8f, 1, 0.9f, 0.7f, 1.2f }; }
        }
    }
}
