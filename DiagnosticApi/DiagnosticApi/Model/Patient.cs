using System.ComponentModel.DataAnnotations;

namespace DiagnosticApi.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Age { get; set; }

    }

}
