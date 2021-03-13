using MO5.Models;
using System;

namespace MO5.Areas.Code.Models
{
  public class Enregistrement : tEnregistrement
  {
    public string Comment1 { get; set; }
    public string Comment2 { get; set; }
    public string Comment3 { get; set; }
    public string Comment4 { get; set; }
    public string Comment5 { get; set; }
    public DateTime? PayDate { get; set; }
    public string Number { get; set; }
    public string BICI { get; set; }
    public string RAccI { get; set; }
    public string BankI { get; set; }
    public string KAccI { get; set; }
    public string INNI { get; set; }
    public string KPPI { get; set; }
    public string NameI { get; set; }
    public string BICO { get; set; }
    public string RAccO { get; set; }
    public string BankO { get; set; }
    public string KAccO { get; set; }
    public string INNO { get; set; }
    public string KPPO { get; set; }
    public string NameO { get; set; }
    public decimal? Amount { get; set; }
    public int? Queue { get; set; }
    public string Reference { get; set; }
    public bool? IsChecked { get; set; }
  }

  public class EnregVM
  {
    public int EnregTypeID { get; set; }
    public int? BeginStatusID { get; set; }
  }
}