﻿using System.Linq;
using System.Threading.Tasks;
using iTechArt.SurveysSite.DomainModel;
using iTechArt.SurveysSite.Foundation;
using iTechArt.SurveysSite.WebApp.Helpers;
using iTechArt.SurveysSite.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.SurveysSite.WebApp.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly ISurveyManagementService _surveyService;


        public SurveyController(ISurveyManagementService surveyService)
        {
            _surveyService = surveyService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetId();
            var surveys = await _surveyService.GetAllUserSurveysAsync(userId);
            var surveyViewModel = surveys.Select(survey => new SurveyViewModel
            {
                Id = survey.Id,
                Title = survey.Title,
                ChangeDate = survey.ChangeDate
            }).ToList();

            return View(surveyViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var surveyViewModel = new SurveyViewModel();

            return View(surveyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SurveyViewModel surveyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(surveyViewModel);
            }

            var survey = ConvertToSurvey(surveyViewModel);

            await _surveyService.CreateSurveyAsync(survey);

            ViewBag.Message = "Survey created successfully";

            return View(surveyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var survey = await _surveyService.GetByIdAsync(id);

            if (!IsUserValid(survey.OwnerId))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            await _surveyService.DeleteSurveyAsync(survey);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var survey = await _surveyService.GetByIdAsync(id);

            if (!IsUserValid(survey.OwnerId))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var surveyQuestionsViewModel = survey.Questions.Select(t => new QuestionViewModel
            {
                QuestionId = t.Id,
                QuestionTitle = t.Title,
                QuestionType = t.QuestionType
            }).ToList();

            var surveyViewModel = new SurveyViewModel
            {
                Id = survey.Id,
                Title = survey.Title,
                Questions = surveyQuestionsViewModel
            };

            return View(surveyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SurveyViewModel surveyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(surveyViewModel);
            }

            var survey = ConvertToSurvey(surveyViewModel);

            if (!IsUserValid(survey.OwnerId))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var fromSurvey = await _surveyService.GetByIdAsync(surveyViewModel.Id);
            await _surveyService.UpdateSurveyAsync(fromSurvey, survey);

            ViewBag.Message = "Survey edited successfully";

            return View(surveyViewModel);
        }


        private Survey ConvertToSurvey(SurveyViewModel surveyViewModel)
        {
            var questions = surveyViewModel.Questions.Select(q => new Question
            {
                Id = q.QuestionId,
                Title = q.QuestionTitle,
                QuestionType = q.QuestionType
            }).ToList();

            var survey = new Survey
            {
                Questions = questions,
                Title = surveyViewModel.Title,
                ChangeDate = surveyViewModel.ChangeDate,
                OwnerId = User.GetId()
            };

            return survey;
        }

        private bool IsUserValid(int id)
        {
            var userId = User.GetId();

            return userId == id;
        }
    }
}