$(function () {
    $('#catId').on("change", function () {
        $('form[name=byCategory]').submit();
    })

    $('#ticketTitle').on("input", function () {
        var data = $(this).val();

        function GetPriority(number) {
            if (number == 1) {
                return "Medium";
            }
            else if(number == 2) {
                return "High";
            }
            else {
                return "Low";
            }
        }

        if (data.length > 1) {
            $.get("/Tickets/Search", { titlePart: data }, function (data) {
                $('#tickets tr').not(':first').remove();
                var html = '';
                $.each(data, function (i, ticket) {
                    html += '<tr><td>' + '<a href=/Home/Details/' + ticket.Id + '>' + ticket.Title + '</a>' +  '</td><td>' + ticket.CategoryName + '</td><td>' + ticket.AuthorName + '</td><td>' + GetPriority(ticket.Priority) + '</td></tr>'
                })
                $('#tickets tr:first').after(html);
                $('.pagination-container').hide();
            });
        }

    })
})