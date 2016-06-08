using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public enum Distribution
    {
        Uniform,
        Gaussian,
        Exponential,
        Gamma,
        Simpson,
        Triangle
    }
    public class GeneratorParamentersViewModel
    {
        [Required]
        [Display(Name="Sequence Length")]
        public int SequenceLength { get; set; }
        [Required]
        [Display(Name="Sequence destribution")]
        public Distribution SequenceDestribution { get; set; }
        [Display(Name = "N")]
        public int N { get; set; }
        [Display(Name = "Mathematical Expectation")]
        public double MathematicalExpectation { get; set; }
        [Display(Name = "Roof-mean square deviation")]
        public double RoofMeanSquareDeviation { get; set; }
        [Display(Name = "Min value")]
        public double Min { get; set; }
        [Display(Name = "Max value")]
        public double Max { get; set; }
        [Display(Name = "η")]
        public int Nu { get; set; }
        [Display(Name = "Gistogramm Segments Count")]
        public int GistogrammSegmentsCount { get; set; }

    }
}