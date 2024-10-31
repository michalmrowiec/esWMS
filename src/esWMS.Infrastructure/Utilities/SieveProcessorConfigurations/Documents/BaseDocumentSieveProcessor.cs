using esWMS.Domain.Entities.Documents;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    public class BaseDocumentSieveProcessor
    {
        public static IQueryable<T> DocumentIssueDateFilter<T>(
            IQueryable<T> source, string op, string[] values)
            where T : BaseDocument
        {
            if (values.Length != 1 || !DateTime.TryParse(values[0], out DateTime dateValue))
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(item => item.DocumentIssueDate.Date == dateValue.Date);
                    break;
                case "!=":
                    source = source.Where(item => item.DocumentIssueDate.Date != dateValue.Date);
                    break;
                case ">":
                    source = source.Where(item => item.DocumentIssueDate.Date > dateValue.Date);
                    break;
                case "<":
                    source = source.Where(item => item.DocumentIssueDate.Date < dateValue.Date);
                    break;
                case ">=":
                    source = source.Where(item => item.DocumentIssueDate.Date >= dateValue.Date);
                    break;
                case "<=":
                    source = source.Where(item => item.DocumentIssueDate.Date <= dateValue.Date);
                    break;
                default:
                    break;
            }
            return source;
        }

        public static IQueryable<T> DocumentIssueDateSort<T>(
            IQueryable<T> source, bool useThenBy, bool desc)
            where T : BaseDocument
        {
            IOrderedQueryable<T> result;

            if (useThenBy)
            {
                result = desc ?
                    ((IOrderedQueryable<T>)source).ThenByDescending(p => p.DocumentIssueDate) :
                    ((IOrderedQueryable<T>)source).ThenBy(p => p.DocumentIssueDate);
            }
            else
            {
                result = desc ?
                    source.OrderByDescending(p => p.DocumentIssueDate) :
                    source.OrderBy(p => p.DocumentIssueDate);

                result = result
                    .ThenBy(p => p.DocumentId)
                    .ThenBy(p => p.IsApproved);
            }

            return result;
        }

    }
}
