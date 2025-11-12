using HelpDesk.Application.Validators;
using HelpDesk.Application.DTOs;
using Xunit;

public class TicketValidatorTests
{
    [Fact]
    public void CreateTicket_WithEmptyTitle_IsInvalid()
    {
        var dto = new CreateTicketDto("Prueba", "Prueba", "alta", "ADmin");
        var v = new CreateTicketValidator();
        var res = v.Validate(dto);
        Assert.False(res.IsValid);
    }
}