
function enableField() {
    
    if ($('#PresenceMotoPerso').is(":checked"))
    {
        $("#conteneurMotoPerso").removeClass("hidden");
        $("#conteneurMotoProposee").addClass("hidden");
    }
    else
    {
        $("#conteneurMotoPerso").addClass("hidden");
        $("#conteneurMotoProposee").removeClass("hidden");
    }

}


$("#MotoPerso").tooltip({ placement: 'right' });
$("#FiltreMaMoto").tooltip({ placement: 'right' });
$("#FiltreMaMoto").attr("placeholder", "Choisir dans la liste : 3 caractères minimum");

$(document).ready(function () {

    enableField();

    $('#MotoProposeeID').select2(
    {
        minimumInputLength: 3,
        theme: "bootstrap",
        language: "fr",
        ajax:
        {
            url: '/Motos/ListePartielleMotos/',
            dataType: 'json',
            delay: 500,
            type: "GET",
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: "Mon deux-roues"
    });

    $('#MotosAccepteesID').select2(
    {
        minimumInputLength: 3,
        multiple: true,
        theme: "bootstrap",
        language: "fr",
        ajax:
        {
            url: '/Motos/ListePartielleMotos/',
            dataType: 'json',
            delay: 500,
            type: "GET",
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: "Les modèles qui m'intéressent"
    });

    $('#MarquesAccepteesID').select2(
    {
        minimumInputLength: 1,
        multiple: true,
        theme: "bootstrap",
        language: "fr",
        ajax:
        {
            url: '/Marques/ListePartielleMarques/',
            dataType: 'json',
            delay: 500,
            type: "GET",
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: "Les marques qui m'intéressent"
    });

    $('#GenresAcceptesID').select2(
    {
        multiple: true,
        theme: "bootstrap",
        placeholder: "Les genres qui m'intéressent"
    });

    $('#DepartementID').select2(
    {
        minimumInputLength: 1,
        theme: "bootstrap",
        language: "fr",
        ajax:
        {
            url: '/Departements/ListePartielleDepartements/',
            dataType: 'json',
            delay: 500,
            type: "GET",
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: "Mon département"
    });

    $(".select2-container").tooltip({
        title: function () {
            return $(this).prev().attr("title");
        },
        placement: "right"
    });

});