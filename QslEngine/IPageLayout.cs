using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    public interface IPageLayout
    {
        string Name { get; }
        int QsoPerLabel { get; }
        int Columns { get; }
        double LabelHeight { get; }
        double PageVerticalMargin { get; }
        double PageHorizontalMargin { get; }
        double LabelPaddingHorizontal { get; }
        float[] ColumnRelativeWidths { get; }
    }
}
