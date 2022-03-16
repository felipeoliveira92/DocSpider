using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocSpider.Domain
{
    public class Document
    {
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
        public byte[] File { get; set; }

        [DisplayName("Tipo")]
        public string ContentType { get; set; }

        [DisplayName("Nome")]
        public string FileName { get; set; }

        [DisplayName("Data de Criação")]
        public DateTime DateCreate { get; set; } = DateTime.Now;
    }
}
