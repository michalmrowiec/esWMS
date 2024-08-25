using esMWS.Domain.Entities.Documents;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    public class BaseDocumentSieveProcessor
    {
        public IQueryable<T> DocumentIssueDateFilter<T>(IQueryable<T> source, string op, string[] values)
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
    }
}
