using LearningPlatform.Application.Teachers.Inputs;
using LearningPlatform.Application.Teachers.Outputs;
using LearningPlatform.Application.Teachers.PersistenceModels;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LearningPlatform.Application.Services;

public interface ITeacherService
{

    //OMBANDLAR HANS KOD SÅ DEN PASSAR I MTT PROJEKT OCH FÖLJER CRUD - INTE SÄKER PÅ OM DETTA STÄMMER !!

    // C
    // TeacherInput
    Task<int> CreateAsync(TeacherInput input, CancellationToken ct = default);

    // R (Hämta EN)
    // TeacherOutput
    Task<TeacherOutput?> GetByIdAsync(int id, CancellationToken ct = default);

    // R ((Hämta ALLA)
    // TeacherOutput
    Task<IReadOnlyList<TeacherOutput>> ListAsync(CancellationToken ct = default);

    // U 
    // TeacherInput
    Task UpdateAsync(int id, TeacherInput input, CancellationToken ct = default);

    // D 
    Task DeleteAsync(int id, CancellationToken ct = default);
}



        // ** HANS KOD **
        //Task<Guid> CreateAsync(CreateUserInput input, CancellationToken ct); //skapade en input i persistence i INPUTS

        //Task<UserOutput?> GetByIdAsync(Guid userId, CancellationToken ct); //skapade en UserOutput i OUTPUTS 

        //Task<IReadOnlyList<UserOutput>> ListAsync(CancellationToken ct = default);

        //Task DeleteAsync(Guid userId, byte[] rowVersion, CancellationToken ct = default);