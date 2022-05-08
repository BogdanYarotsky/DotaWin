using MediatR;
using DotaWin.DataTransformer.Models;

namespace DotaWin.DataTransformer.Queries;

public class HeroesList
{
    public class Query : IRequest<List<Hero>>
    {

    }

}
