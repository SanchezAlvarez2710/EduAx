﻿
/* Script scrl animation */
ScrollReveal({
    reset: false,
    distance: '60px',
    duration: 1500,
    delay: 600
});
//MAIN
ScrollReveal().reveal('.main-title', { delay: 400, origin: 'right' });
ScrollReveal().reveal('.img-course', { delay: 600, origin: 'top' });
ScrollReveal().reveal('.course-status', { delay: 500, origin: 'bottom' });
ScrollReveal().reveal('.course-attempts', { delay: 600, origin: 'bottom' });
ScrollReveal().reveal('.teacher-status', { delay: 700, origin: 'bottom' });
ScrollReveal().reveal('.group-status', { delay: 800, origin: 'bottom' });
ScrollReveal().reveal('.desc', { delay: 900, origin: 'bottom' });
ScrollReveal().reveal('.content', { delay: 1000, origin: 'bottom' });
ScrollReveal().reveal('.dash-top .btn-action', { delay: 1100, origin: 'bottom' });
////MODAL
//ScrollReveal().reveal('.modal-title', { delay: 500, origin: 'right' });
//ScrollReveal().reveal('hr', { delay: 600, origin: 'bottom' });
//ScrollReveal().reveal('.modal-img', { delay: 700, origin: 'left' });
////MODAL -> GRADES
//ScrollReveal().reveal('.average-grade', { delay: 700, origin: 'bottom', reset: true });
//ScrollReveal().reveal('.grade1', { delay: 800, origin: 'bottom', reset: true });
//ScrollReveal().reveal('.grade2', { delay: 900, origin: 'bottom', reset: true });
//ScrollReveal().reveal('.grade3', { delay: 1000, origin: 'bottom', reset: true });
//ScrollReveal().reveal('.grade4', { delay: 1100, origin: 'bottom', reset: true });
//ScrollReveal().reveal('.modal-close', { delay: 1500, origin: 'left', reset: true });

//OPEN-MODAL-GRADES
var openModalGrades = document.querySelector('.hero-cta');
var modalGrades = document.querySelector('.modal-grades');
var closeModalGrades = document.querySelector('.modal-grades-close');

if (openModalGrades != null && modalGrades != null && closeModalGrades != null) {
    openModalGrades.addEventListener('click', (e) => {
        e.preventDefault();
        modalGrades.classList.add('modal-show');
    });

    closeModalGrades.addEventListener('click', (e) => {
        e.preventDefault();
        modalGrades.classList.remove('modal-show');
    });
}

//OPEN-MODAL-ENROLL
var openModalEnroll = document.querySelector('.btn-action');
var modalEnroll = document.querySelector('.modal-enroll');
var closeModalEnroll = document.querySelector('.modal-enroll-close');

if (openModalEnroll != null && modalEnroll != null && closeModalEnroll != null) {
    openModalEnroll.addEventListener('click', (e) => {
        e.preventDefault();
        modalEnroll.classList.add('modal-show');
    });

    closeModalEnroll.addEventListener('click', (e) => {
        e.preventDefault();
        modalEnroll.classList.remove('modal-show');
    });
}

