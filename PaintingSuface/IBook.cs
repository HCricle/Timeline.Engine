using System;
using System.Collections.Generic;

namespace Timeline.Framework.Drawing.PaintingSuface
{
    public interface IBook:IDisposable
    {
        Page ActivePage { get; }
        Type ActivePageType { get; }
        int ActivePageTypeIndex { get; }
        bool HasActivePage { get; }

        void AddPage<TPage>() where TPage : Page;
        IReadOnlyCollection<Type> GetPageTypes();
        void RemovePage<TPage>() where TPage : Page;
        void SetActivePage(int index);
        void SetEmptyPage();
    }
}