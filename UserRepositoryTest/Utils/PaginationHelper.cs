namespace UserRepositoryTest.Utils;

public class PaginationHelper
{
    public const int MAX_VISIBLE_PAGES = 5;

    public static IEnumerable<int> GetPageNumbers(int currentPage, int maxPageNumber)
    {
        int startPage = Math.Max(1, currentPage - MAX_VISIBLE_PAGES / 2);
        int endPage = Math.Min(maxPageNumber, startPage + MAX_VISIBLE_PAGES - 1);

        if (endPage - startPage + 1 < MAX_VISIBLE_PAGES)
        {
            startPage = Math.Max(1, endPage - MAX_VISIBLE_PAGES + 1);
        }

        return Enumerable.Range(startPage, endPage - startPage + 1);
    }
}
