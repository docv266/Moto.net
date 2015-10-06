
function enableField() {
    
    $("#conteneurMotoPerso").toggleClass("hidden");

    $("#conteneurMotoProposee").toggleClass("hidden");

}

$(".chosen-select").chosen({ placeholder_text_multiple: "Choisir plusieurs éléments", placeholder_text_single: "Choisir un élément", no_results_text: "Pas de résultat pour" });

$('.chosen-container').tooltip({ placement: 'right' });
$("#MotoPerso").tooltip({ placement: 'right' });
$("#FiltreMaMoto").tooltip({ placement: 'right' });
$("#FiltreMaMoto").attr("placeholder", "Choisir dans la liste : 3 caractères minimum");


$(document).ready(function () {

    $('#MotosAccepteesID').select2(
    {
        minimumInputLength: 3,
        multiple: true,
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
        placeholder: "Choisir dans la liste : 3 caractères minimum"
    });

    $('#MotoProposeeID').select2(
    {
        minimumInputLength: 3,
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
        placeholder: "Choisir dans la liste : 3 caractères minimum"
    });

});
