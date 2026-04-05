using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.DataStore.Paging
{
    public class Pagination
    {
        private long _totalItem = 0;
        private int _pageSize = 10;
        private int _current = 1;
      
        public Pagination(long totalItems, int itemsPerPage,int currentPage)
        {
            Current = currentPage;
            TotalItems = totalItems;
            PageSize = itemsPerPage;
        }

        public int Current
        {
            get
            {
                return this._current;
            }
            set
            {
                validateCurrent();
                this._current = value;   
            }
        }

        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Page size above 0");
                else
                {
                    this.validateCurrent();
                    this._pageSize = value;
                }
            }
        }

        public long TotalItems
        {
            get
            {
                return this._totalItem;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Total item above 0");
                else
                {
                    this.validateCurrent();
                    this._totalItem = value;
                }
            }
        }

        public long TotalPage
        {
            get
            {
                long totalPages = this.TotalItems % this.PageSize;

                if (totalPages == 0 || (this.TotalItems % this.PageSize) != 0)
                    totalPages++; // Last page with only 1 item left

                return totalPages;
            }
        }
      
        public long RecordFrom
        {
            get
            {
                return (Current - 1) * PageSize;
            }
        }

        public long RecordTo
        {
            get
            {
                return (Current * PageSize) - 1;
            }
        }

        private void validateCurrent()
        {
            if (this.Current < 0 || this.Current > this.TotalPage)
                throw new Exception($"Current is between 0 and {this.TotalPage}");
        }
    }
}
