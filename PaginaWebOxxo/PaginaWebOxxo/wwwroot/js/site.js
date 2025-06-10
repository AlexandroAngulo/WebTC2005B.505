// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function habilitartelefono() 
    {
        var input = document.getElementById("telefonoinput");
        input.removeAttribute("disabled");
        input.focus();
    }

  document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('form[action*="EquiparPersonaje"]').forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault();
            const modal = new bootstrap.Modal(document.getElementById('empleadoModal'));
            modal.show();
            setTimeout(() => {
                form.submit();
            }, 2000);
        });
    });
    
    document.querySelectorAll('form[action*="EquiparTrack"]').forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault();
            const modal = new bootstrap.Modal(document.getElementById('empleadoModal'));
            modal.show();
            setTimeout(() => {
                form.submit();
            }, 2000);
        });
    });
});

