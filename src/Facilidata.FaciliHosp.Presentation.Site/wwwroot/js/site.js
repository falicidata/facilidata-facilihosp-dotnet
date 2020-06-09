(function ($) {
    $(document).ready(function () {

        if ($('#tiposExame').length > 0) {
            $.ajax({
                method: "GET",
                url: "/Exame/Tipos",
            }).done(function (retorno) {
                retorno.forEach(function (item) {
                    $('#tiposExame').append('<option value="' + item.tipoOutro + '">');
                });
            });
        }  
    });
})(jQuery);