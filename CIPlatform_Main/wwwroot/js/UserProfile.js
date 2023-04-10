﻿const fileInput = document.getElementById("file-input");const imagePreview = document.getElementById("image-preview");const uploadedFiles = new Set();fileInput.addEventListener("change", () => {    const files = fileInput.files;    handleFiles(files);});function handleFiles(files) {    for (let i = 0; i < files.length; i++) {        const file = files[i];        if (!file.type.startsWith("image/") && !file.type.startsWith("video/")) continue;        if (uploadedFiles.has(file.name)) {            alert(`File "${file.name}" has already been uploaded.`);            continue;        }        uploadedFiles.add(file.name);        const image = document.createElement("img");        image.classList.add("image-preview");        const imageContainer = document.createElement("div");        imageContainer.classList.add("image-container");        const removeImage = document.createElement("div");        removeImage.innerHTML = "&#10006;";        removeImage.classList.add("remove-image");        removeImage.addEventListener("click", () => {            uploadedFiles.delete(file.name);            imageContainer.remove();        });        const reader = new FileReader();        reader.readAsDataURL(file);        reader.onload = () => {            image.src = reader.result;            imageContainer.appendChild(image);            imageContainer.appendChild(removeImage);            imagePreview.appendChild(imageContainer);        };    }}// Skiils section Js$(function () {    var skillList = [];    var skillId = [];    var actives = '';    $('body').on('click', '.list-group .list-group-item', function () {        $(this).toggleClass('active-skill');    });    $('.list-arrows a').click(function () {        var $a = $(this);        if ($a.hasClass('move-left')) {            actives = $('.list-right ul li.active-skill');            actives.clone().appendTo('.list-left ul');            actives.remove();            if ($('.list-left ul li').hasClass('active-skill')) {                $('.list-left ul li').removeClass('active-skill');            }        } else if ($a.hasClass('move-right')) {            actives = $('.list-left ul li.active-skill');            actives.clone().appendTo('.list-right ul');            actives.remove();            if ($('.list-right ul li').hasClass('active-skill')) {                $('.list-right ul li').removeClass('active-skill');            }        }        skillList = [];        $('.list-right ul li').map(function () {            skillList.push($(this).text());        });        skillId = [];        $('.list-right ul li').map(function () {            skillId.push($(this).val());        });        console.log(skillList);        console.log(skillId);    });    $('#save-skills').on('click', function () {        $('#add-skills').modal('toggle');        $('#selectedSkills').html('');        if (skillList.length > 0) {            for (var i = 0; i < skillList.length; i++) {                $('#selectedSkills').append('<small class="mb-2">' + skillList[i] + '</small>');            }        }        //else {        //    $('#selectedSkills').append('<small class="mb-2 text-danger">No Skills Selected</small>');        //}        if (skillId.length > 0) {            for (var i = 0; i < skillId.length; i++) {                let inputElement = $('<input>', {                    type: 'hidden',                    value: skillId[i],                    name: 'finalSkillList'                });                $('#selectedSkills').append(inputElement);            }        }    });    if (skillList.length == 0) {        $('#selectedSkills').append('<small class="mb-2 text-danger">No Skills Selected</small>');    }    $('[name="SearchDualList"]').keyup(function (e) {        var code = e.keyCode || e.which;        if (code == '9') return;        if (code == '27') $(this).val(null);        var $rows = $(this).closest('.dual-list').find('.list-group li');        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();        $rows.show().filter(function () {            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();            return !~text.indexOf(val);        }).hide();    });    $(window).on('resize', function () {        var win = $(this);        if (win.width() < 991) {            $('.list-arrows .move-right i').removeClass('bi-caret-right-fill').addClass('bi-caret-down-fill');            $('.list-arrows .move-left i').removeClass('bi-caret-left-fill').addClass('bi-caret-up-fill');        } else {            $('.list-arrows .move-right i').addClass('bi-caret-right-fill').removeClass('bi-caret-down-fill');            $('.list-arrows .move-left i').addClass('bi-caret-left-fill').removeClass('bi-caret-up-fill');        }    });});