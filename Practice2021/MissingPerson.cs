//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practice2021
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class MissingPerson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MissingPerson()
        {
            this.SearchCampaigns = new HashSet<SearchCampaign>();
        }

        public int MissingPersonID { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Фамилия")]
        [MaxLength(30, ErrorMessage = "Вы превысили возможное количество символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Имя")]
        [MaxLength(30, ErrorMessage = "Вы превысили возможное количество символов")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        [MaxLength(30, ErrorMessage = "Вы превысили возможное количество символов")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        [Range(typeof(DateTime), "1-1-1921", "1-1-2020")]
        public System.DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        //[Range(typeof(int), "100", "250")]
        public byte Growth { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string BodyType { get; set; }

        [MaxLength(250, ErrorMessage = "Вы превысили возможное количество символов")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string Appearance { get; set; }

        [MaxLength(250, ErrorMessage = "Вы превысили возможное количество символов")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string ClothingDescription { get; set; }

        [MaxLength(250, ErrorMessage = "Вы превысили возможное количество символов")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string SpecialThings { get; set; }


        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateLastSeen { get; set; }

        [MaxLength(200, ErrorMessage = "Вы превысили возможное количество символов")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string PlaceLastSeen { get; set; }

        [MaxLength(200, ErrorMessage = "Вы превысили возможное количество символов")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string PossibleLocation { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
       
        [Range(typeof(DateTime), "1-1-1921","1-1-2022")]
        public Nullable<System.DateTime> DateFound { get; set; }

        [MaxLength(60, ErrorMessage = "Вы превысили возможное количество символов")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string FullApplicant { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage ="Неправильный номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Неправильный адрес электронной почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool New { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]

        public byte[] Image { get; set; }
    
        public virtual Gender Gender1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SearchCampaign> SearchCampaigns { get; set; }
    }
}