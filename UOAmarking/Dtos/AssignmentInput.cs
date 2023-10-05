using System;
using UOAmarking.Models;

namespace UOAmarking.Dtos
{
	public class AssignmentInput
	{
        public string assignmentType { get; set; }

        public string description { get; set; }

        public Course Course { get; set; }
    }
}

