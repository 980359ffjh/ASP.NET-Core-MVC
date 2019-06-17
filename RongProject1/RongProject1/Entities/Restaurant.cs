using System.ComponentModel.DataAnnotations;

namespace RongProject1.Entities
{
    public enum CuisineType
    {
        None,
        Italian,
        French,
        Japanese,
        Taiwan
    }

    public class Restaurant
    {
        public int Id { get; set; }

        // ***** 一定要放在要改變的變數上面 *****
        [Display(Name = "Restautant Name")] //change Name's innerText
        //[DataType(DataType.Password)] //cahnge data type -> password 型態
        
        [Required, MaxLength(80)] //驗證方式(Required: 一定要填寫) and set input's length limit
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}
