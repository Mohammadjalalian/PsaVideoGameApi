using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsaVideoGameCommon
{
  public class VideoGame : IEntity
  {
    public int Id { get; set; }
    

    [StringLength(50)]
    public string Name { get; set; }
    [StringLength(50)]
    public string Genre { get; set; }
    public string Description { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreationTime { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ModificationTime { get; set; }
  }
}
