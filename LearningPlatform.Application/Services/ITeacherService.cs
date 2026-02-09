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

    // C - Create (Skapa)
    Task CreateAsync(TeacherModel model, CancellationToken ct = default);

    // R - Read (Hämta en)
    Task<TeacherModel?> GetByIdAsync(int id, CancellationToken ct = default);

    // R - Read (Hämta alla)
    Task<IReadOnlyList<TeacherModel>> ListAsync(CancellationToken ct = default);

    // U - Update (Uppdatera)
    Task UpdateAsync(TeacherModel model, CancellationToken ct = default);

    // D - Delete (Ta bort)
    Task DeleteAsync(int id, CancellationToken ct = default);

}





        // ** HANS KOD **
        //Task<Guid> CreateAsync(CreateUserInput input, CancellationToken ct); //skapade en input i persistence i INPUTS

        //Task<UserOutput?> GetByIdAsync(Guid userId, CancellationToken ct); //skapade en UserOutput i OUTPUTS 

        //Task<IReadOnlyList<UserOutput>> ListAsync(CancellationToken ct = default);

        //Task DeleteAsync(Guid userId, byte[] rowVersion, CancellationToken ct = default);