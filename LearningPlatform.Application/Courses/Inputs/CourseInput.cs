using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Courses.Inputs
{
    // NY RECORD

    public sealed record CourseInput(
        int CourseCode,
        string Title,
        string Description
        );


}









    // GAMMAL KLASS
    /*public class CourseInput
    {
        public int CourseCode { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }*/

