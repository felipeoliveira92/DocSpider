using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocSpider.Domain
{
    public class Document
    {
        public Document(string title, string description, byte file, string fileName)
        {
            this.Title = title;
            this.Description = description;
            this.File = file;
            this.FileName = fileName;
            DateCreate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Titulo")]
        [Required(ErrorMessage = "Campo Requerido!")]
        [MaxLength(100)]
        public string Title { get; set; }

        [DisplayName("Descrição")]
        [MaxLength(2000)]
        public string Description { get; set; }

        [DisplayName("Arquivo")]
        public byte File { get; set; }

        [DisplayName("Nome")]
        public string FileName { get; set; }

        [DisplayName("Data de Criação")]
        public DateTime DateCreate { get; set; }

    }
}
