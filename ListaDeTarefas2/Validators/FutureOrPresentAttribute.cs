using System.ComponentModel.DataAnnotations;

namespace ListaDeTarefas2.Validators
{
    public class FutureOrPresentAttribute : ValidationAttribute
    {
        public FutureOrPresentAttribute()
        {
            ErrorMessage = "A data deve ser no futuro ou no presente";
        }

        public override bool IsValid(object? value)
        {
            if(value is null)
            {
                return true;
            }
            var date = (DateTime)value;
            return date >= DateTime.Now.Date;
        }
    }
}
