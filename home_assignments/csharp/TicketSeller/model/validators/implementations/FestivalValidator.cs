using System;
using model.entities;
using model.validators.interfaces;

namespace model.validators.implementations;

public class FestivalValidator:IFestivalValidator
{
    public void Validate(Festival festival)
    {
        throw new NotImplementedException();
    }
    public void ValidateId(int id)
    {
        if (id == null) {
            throw new ValidationException("ID is null;\n");
        }
    }

    public void ValidateDate(DateTime date)
    {
        if (date == null) {
            throw new ValidationException("Date is null;\n");
        }
    }
}