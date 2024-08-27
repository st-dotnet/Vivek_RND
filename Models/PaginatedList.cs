using System.Collections;
using System.Collections.Generic;

namespace WebForm.Models
{
    public class PaginatedList<T> : IEnumerable<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        // Implement GetEnumerator for IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        // Explicitly implement non-generic IEnumerable interface
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
