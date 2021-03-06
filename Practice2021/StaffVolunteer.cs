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

    public partial class StaffVolunteer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaffVolunteer()
        {
            this.Groups = new HashSet<Group>();
        }
    
        public int VolunteerID { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1-1-1921", "1-1-2020")]
        public System.DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Неправильный номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Неправильный адрес электронной почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string Password { get; set; }
        public int SuperUser { get; set; }
        public bool BeReady { get; set; }
    
        public virtual Gender Gender1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
