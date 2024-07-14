﻿using Application.TrainingMaterials.Queries.DownloadTrainingMateria;
using Application.TrainingMaterials.Queries.GetPagedTrainingMaterials;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class TrainingMaterialController : CustomBaseController
{
    private readonly IMediator _mediator;

    public TrainingMaterialController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedItems(
        string? keyword,
        SortType sortType = SortType.Ascending,
        int pageIndex = 0,
        int pageSize = 10)
    => Ok(await _mediator.Send(new GetPagedTrainingMaterialsQuery()
    {
        Keyword = keyword,
        PageIndex = pageIndex,
        PageSize = pageSize,
        SortType = sortType
    }));

    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(int id)
    => await _mediator.Send(new DownloadTrainingMaterialQuery(id));

    [HttpGet("{id}/share")]
    public RedirectResult Share(int id)
    {
        return RedirectPermanent($"https://qr.softvn.com?q=trainingmanagementsystem.azurewebsites.net/api/TrainingMaterial/{id}/download");
    }
}
