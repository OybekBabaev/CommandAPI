using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Dtos;

#pragma warning disable 8618

public class CommandUpdateDto
{
    [Required]
    [MaxLength(250)]
    public string HowTo {get;set;}
    
    [Required]
    public string Platform {get;set;}

    [Required]
    public string CommandLine {get;set;}
}