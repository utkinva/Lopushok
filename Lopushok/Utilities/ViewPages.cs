using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lopushok.Utilities
{
    class ViewPages
    {
        public int pageNumber { get; set; }
        public int startIndex { get; set; }
        public int rangeItemsCount { get { return 20; } }
        public int itemsCount { get; set; }

        public ViewPages(int _pageNumber, int _startIndex)
        {
            pageNumber = _pageNumber;
            startIndex = _startIndex;
        }

        public int GetTotalPage()
        {
            return (int)Math.Ceiling((decimal)itemsCount / rangeItemsCount);
        }

        public bool LessCountRangeItems
        {
            get
            {
                return (itemsCount < rangeItemsCount);
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (pageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (pageNumber <= GetTotalPage());
            }
        }

        public void GetIndex()
        {
            startIndex = 0;

            if (HasPreviousPage && HasNextPage)
            {
                for (int i = 1; i < pageNumber; i++)
                {
                    startIndex += rangeItemsCount;
                }
            }
        }
    }

}

