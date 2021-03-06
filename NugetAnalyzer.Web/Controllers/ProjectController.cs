﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NugetAnalyzer.BLL.Interfaces;
using NugetAnalyzer.DTOs.Models.Reports;

namespace NugetAnalyzer.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public async Task<IActionResult> Report(int id)
        {
           ProjectReport model = await projectService.GetProjectReportAsync(id);

           if (model == null)
           {
               return BadRequest();
           }

            return View(model);
        }
    }
}