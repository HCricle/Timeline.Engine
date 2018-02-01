using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Image;

namespace Timeline.Framework.Drawing.PaintingSuface
{
    /// <summary>
    /// 表示一本书,一个游戏抽象于一本书,一页纸就是一个场景,其中书可以是一本只读故事书,还是其它书
    /// </summary>
    public class Book : IBook,IDrawable
    {
        /// <summary>
        /// 存的是书页的内容
        /// </summary>
        private List<Type> PageTypes;
        private int activePageTypeIndex;
        private Type activePageType;
        private Page activePage;
        private bool isInit;
        /// <summary>
        /// 当前激活的页类型
        /// </summary>
        public Type ActivePageType => activePageType;
        /// <summary>
        /// 当前激活的页实例
        /// </summary>
        public Page ActivePage => activePage;
        /// <summary>
        /// 当前激活的页引索
        /// </summary>
        public int ActivePageTypeIndex => activePageTypeIndex;
        /// <summary>
        /// 是否有激活页
        /// </summary>
        public bool HasActivePage => ActivePage != null;
        public Book()
        {
            PageTypes = new List<Type>();
            activePageTypeIndex = -1;
        }
        /// <summary>
        /// 加入页(场景)
        /// </summary>
        public void AddPage<TPage>()
            where TPage:Page
        {
            PageTypes.Add(typeof(TPage));
        }
        /// <summary>
        /// 不建议使用,因为一个场景只会有一个页激活,其它页只是关闭(只删除第一个相关的页)
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        public void RemovePage<TPage>()
             where TPage : Page
        {
            var itp = typeof(TPage).FullName;
            var p = PageTypes.Where(pg => pg.FullName == itp);
            if (p == null) 
            {
                throw new ArgumentException($"{itp}-不存在此页");
            }
            PageTypes.Remove(p.ElementAt(0));            
        }
        public IReadOnlyCollection<Type> GetPageTypes()
            => PageTypes;
        /// <summary>
        /// 设置翻到的页,同时或释放当前页,并且加载指定页
        /// </summary>
        /// <param name="index"></param>
        public void SetActivePage(int index)
        {
            if (index > PageTypes.Count - 1 || index < 0)
            {
                throw new ArgumentException($"无法找到第{index}页");
            }
            activePageTypeIndex = index;
            if (HasActivePage)
            {
                activePage?.Dispose();
            }
            CanvasImgCacher.Dispose();
            activePageType = PageTypes[activePageTypeIndex];
            activePage = (Page)Activator.CreateInstance(activePageType,this);

        }
        /// <summary>
        /// 设置空页
        /// </summary>
        public void SetEmptyPage()
        {
            activePageTypeIndex = -1;
            if (HasActivePage)
            {
                activePage.Dispose();
            }
            activePage.Dispose();
            CanvasImgCacher.Dispose();
            activePageType = null;
            activePage = null;
        }
        public void Update(ICanvasResourceCreator creator, CanvasTimingInformation time)
        {
            if (!isInit)
            {
                isInit = true;
                CreateResources(creator);
            }
            if (HasActivePage)
            {
                ActivePage.Update(creator, time);
            }
        }
        public void Draw(ICanvasResourceCreator creator, CanvasAnimatedDrawEventArgs args)
        {
            if (HasActivePage)
            {
                ActivePage.Draw(creator, args);
            }
        }
        /// <summary>
        /// 这里创建的资源在整个游戏中都会存在,可以创建主角等等的物体
        /// </summary>
        /// <param name="creator"></param>
        public virtual void CreateResources(ICanvasResourceCreator creator) { }
        public void Dispose()
        {
            SetEmptyPage();
        }
    }
}
