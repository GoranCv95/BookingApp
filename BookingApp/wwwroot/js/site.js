// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('.table').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/sr-SP.json',
        },
    });
    $('.korisnikSelect').select2();
    $('.smjestajSelect').select2();
    $('.tipSmjestaja').select2();
});