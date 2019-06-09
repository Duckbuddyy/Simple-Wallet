using System.Collections.Generic;
using WebTabanliProje.Models;

namespace WebTabanliProje.Areas.Users.ViewModels
{
    public class RecordsIndex
    {
        public IEnumerable<Record> Records { get; set; }
        public List<string> CategoryString { get; set; }
    }
    public class RecordsCreate
    {
        public float Amount { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Note { get; set; }
    }
    public class RecordsEdit
    {
        public int Record_Id { get; set; }
        public float Amount { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Note { get; set; }
    }
}