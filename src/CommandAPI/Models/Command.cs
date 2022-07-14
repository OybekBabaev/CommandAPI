using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Models;

#pragma warning disable 8618

public class Command
{
    [Key]
    [Required]
    public int Id {get;set;}

    [Required]
    [MaxLength(250)]
    public string HowTo {get;set;}

    [Required]
    public string Platform {get;set;}

    [Required]
    public string CommandLine {get;set;}
}