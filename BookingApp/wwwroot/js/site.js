﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('.table').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/sr-SP.json',
        },
    });

    $('.korisnikSelect').select2({
        placeholder: 'Odaberi korisnika',
        allowClear: true,
        ajax: {
            url: "/Korisnici/DohvatiKorisnike/",
            dataType: 'json',
            delay: 250,
            processResults: function (data) {
                return {
                    results: $.map(data, function (korisnik) {
                        return {
                            id: korisnik.korisnikId,
                            text: korisnik.ime + ' ' + korisnik.prezime
                        };
                    })
                };
            },
            cache: true
        }
    });
    $('.smjestajSelect').select2();
    $('.tipSmjestaja').select2();
});