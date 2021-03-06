﻿// Effacer les données du formulaire de filtrage
function clearForm() {

    // Efface les valeurs des champs text et number de la form #formFilter
    $("form#formFilter input[type=text],form#formFilter input[type=number], #FiltreIdMaMoto").each(function () {
        var input = $(this);
        input.val("");
    });

    $('#RegionsID').select2("data", null);
    $('#RegionsID').select2("val", "");
    $('#DepartementsID').select2("data", null);
    $('#DepartementsID').select2("val", "");
    $('#MaMotoID').select2("val", "");
}

// Conserver l'état de la div de filtrage
function retainDivCollapsedState(nameOfDiv, nameOfHeader)
{
    // when the div is shown, save a cookie with a value of 'true'
    $("#" + nameOfDiv).on('shown.bs.collapse', function () {
        Cookies.set(nameOfDiv, "true"); // this is a cookie.  named the same as the control (poor practice) for brevity 
    });
    // when the div is collapsed, remove the same cookie
    $("#" + nameOfDiv).on('hidden.bs.collapse', function () {
        Cookies.set(nameOfDiv, "false")
    });

    // on page load, show or hide the div (and stylized the header) according to the cookie (if it exists)
    if (Cookies.get(nameOfDiv) == "false") {
        $("#" + nameOfDiv).removeClass("in");                      // The div to show
        $("#" + nameOfHeader).addClass("collapsed");         // The header to stylize as expanded
    }
}
