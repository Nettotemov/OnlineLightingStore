/* Обработчик клика на чекбоксах */
$('input[data-check]').on('input', function () {
	let aChecked = [];
	$('input[data-check]:checked').each(function () { aChecked.push($(this).data('check')); });
	localStorage.setItem('CheckboxChecked', aChecked.join(';'));
	localStorage.setItem('PasswordLength', $('#height_opt').val());
});

/* Установка состояний чекбоксов, после загрузки страницы */
// $(document).ready(function () {
// 	if (localStorage.getItem('CheckboxChecked')) {
// 		$('input[data-check]').prop('checked', false);
// 		let aChecked = localStorage.getItem('CheckboxChecked').split(';');
// 		aChecked.forEach(function (str) { $('input[data-check="' + str + '"]').prop('checked', true); });
// 	}
// 	if (localStorage.getItem('PasswordLength')) {
// 		$('#height_opt').val(localStorage.getItem('PasswordLength'));
// 	}
// });

(function burgerAnimation() {
	$('.burger').on('click', function () {
		$('.burger-line').toggleClass('burger-line__active');
		$('.burger-line-center').toggleClass('burger-line-center__active');
	})
})();
document.querySelector(".burger").addEventListener('click', function () {
	document.querySelector(".menu").classList.toggle("menu__active");
}, false);

/* Установка состояний чекбоксов, при переходе на сайт по ссылке */
function autoCheck() {
	const urlParams = new URL(window.location.href);
	let checked = urlParams.searchParams.getAll("tags");
	let colorChecked = urlParams.searchParams.getAll("color");
	let typesChecked = urlParams.searchParams.getAll("types");
	let materialsChecked = urlParams.searchParams.getAll("materials");
	if (!checked || !colorChecked || !typesChecked || !materialsChecked) {
		return;
	}
	for (let i = 0; i < checked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + checked[i] + '"]').checked = true;
	}
	for (let i = 0; i < colorChecked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + colorChecked[i] + '"]').checked = true;
	}
	for (let i = 0; i < typesChecked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + typesChecked[i] + '"]').checked = true;
	}
	for (let i = 0; i < materialsChecked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + materialsChecked[i] + '"]').checked = true;
	}
	if (!target) {
		return;
	}
	target.checked = true;
}
autoCheck();

const createURL = () => {
	let urlSearch = new URLSearchParams()
	let name = $("[name=name]:input").map((_, select) => select.value).get()
	let category = $("[name=category]").map((_, select) => select.value).get()
	let types = $("[name=types]:checked").map((_, chk) => chk.value).get()
	let materials = $("[name=materials]:checked").map((_, chk) => chk.value).get()
	let tags = $("[name=tags]:checked").map((_, chk) => chk.value).get()
	let color = $("[name=color]:checked").map((_, chk) => chk.value).get()
	let sorted = $("[name=sortOrder]").map((_, select) => select.value).get()
	let minPrice = $("[name=minPrice]:input").map((_, minPrice) => minPrice.value).get()
	let maxPrice = $("[name=maxPrice]:input").map((_, maxPrice) => maxPrice.value).get()
	if (name.length > 0) urlSearch.set("name", name.join("&name="))
	if (category.length > 0) urlSearch.set("category", category.join("&category="))
	if (types.length > 0) urlSearch.set("types", types.join("&types="))
	if (materials.length > 0) urlSearch.set("materials", materials.join("&materials="))
	if (tags.length > 0) urlSearch.set("tags", tags.join("&tags="))
	if (color.length > 0) urlSearch.set("color", color.join("&color="))
	if (sorted.length > 0) urlSearch.set("sortOrder", sorted.join("&sortOrder="))
	if (minPrice > 0) urlSearch.set("minPrice", minPrice.join("&minPrice="))
	if (maxPrice > 0) urlSearch.set("maxPrice", maxPrice.join("&maxPrice="))
	const srch = '?' + urlSearch.toString();
	console.log(srch);
	window.history.pushState({}, '', srch.replace(/%26/g, "&").replace(/%3D/g, "="));
};
$(function () {
	$("input:checkbox, [name=category], [name=sortOrder], [name=minPrice], [name=maxPrice]").on("change click", createURL)
});



$(document).on("submit", ".addToCart", function (ev) {
	var frm = $(this);
	$.ajax({
		type: 'POST',
		url: '/Cart',
		data: frm.serialize(),
		dataType: "json",
		success: function (str) {
			let jsonCart = str;
			$('#quantity').html(jsonCart.quantity);
		}
	});
	ev.preventDefault();
});


$(document).on("click", ".pagination-btn", function (ev) {
	$('#spinner-border').show();
	let path = ev.target.baseURI.split("?"); // забираем путь
	let page = ev.target.value;
	console.log(ev);
	var token = GetAntiForgeryToken();
	let prodview = document.getElementById('prod-view');
	if (typeof page !== 'undefined') {
		$.ajax({
			type: 'GET',
			url: '/catalog/' + page + "?handler=Products",
			data: path[1], // забираем страницу, которую нужно отобразить
			dataType: 'json',
			success: function (pages) {
				let getUrl = '/catalog/' + page + "?" + path[1];
				let pagingJson = JSON.parse(pages.pagingInfoJson)
				let paging = document.getElementById('pagingbuttons');
				$('#pagingbuttons').empty();
				pagingButtons = document.createElement("li");
				pagingButtons.classList.add("page-item");
				if (pagingJson.CurrentPage <= 1) {
					pagingButtons.innerHTML = `<button form="catalogForm" class="pagination-btn page-link" type="submit" value="1" formaction="/1">Назад</button>`
				}
				else {
					pagingButtons.innerHTML = `<button form="catalogForm" class="pagination-btn page-link" type="submit" value="${pagingJson.CurrentPage - 1}" formaction="/${pagingJson.CurrentPage - 1}">Назад</button>`
				}
				paging.appendChild(pagingButtons);
				if (pagingJson.CurrentPage <= 1) {
					pagingButtons.classList.add("disabled");
				}

				for (let i = 1; i <= pagingJson.TotalPages; i++) {
					pagingButtons = document.createElement("li");
					pagingButtons.classList.add("page-item");
					pagingButtons.innerHTML = `<button form="catalogForm" class="pagination-btn page-link" type="submit" value="${i}" formaction="/${i}">${i}</button>`
					paging.appendChild(pagingButtons);
					if (pagingJson.CurrentPage == page && i == page) {
						pagingButtons.classList.add("active");
					}
				}

				pagingButtons = document.createElement("li");
				pagingButtons.classList.add("page-item");
				pagingButtons.innerHTML = `<button form="catalogForm" class="pagination-btn page-link" type="submit" value="${pagingJson.CurrentPage + 1}" formaction="/${pagingJson.CurrentPage + 1}">Вперёд</button>`
				paging.appendChild(pagingButtons);
				if (pagingJson.CurrentPage >= pagingJson.TotalPages) {
					pagingButtons.classList.add("disabled");
				}

				window.history.pushState({}, '', getUrl); // устанавливаем URL в строку браузера
				let productJson = JSON.parse(pages.json)
				let postWrp;
				if (productJson.length == 0) {
					$('#prod-view').empty();
					postWrp = document.createElement("div");
					postWrp.classList.add("col-12");
					postWrp.innerHTML = `<div class="" id="">
					<p class="">Нет товаров</p></div>`
					prodview.append(postWrp);
					$('#spinner-border').hide();
				}
				if (productJson.length > 0) {
					$('#prod-view').empty();
					for (let i = 0; i < productJson.length; i++) {
						let price = productJson[i].Price;
						let oldPrice = productJson[i].OldPrice;
						postWrp = document.createElement("div");
						postWrp.classList.add("search-card__wrapper");
						postWrp.innerHTML = `<a class="search-card__info" href="/catalog/${productJson[i].Category.CategoryName}/${productJson[i].Name}/${productJson[i].ProductID}">
							<div id="img-container">
    							<div class="search-card__image-wrapper">
    								<img class="search-card__image" src="${productJson[i].MainPhoto}">
 								</div>
  							</div>
  							<div class="footer-card__wrapper">
								<p class="footer-card__text">${productJson[i].Name}</p>
								<p class="footer-card__price">${price.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' })}</p>
    						</div>
						</a>
						<form id="ProductID=${productJson[i].ProductID}" class="addToCart" method="post" action="/Cart" >
							<input type="hidden" name="ProductID" id="pr_ProductID" value="${productJson[i].ProductID}" />
							<input type="hidden" name="returnUrl" value="${document.location.href}" />
							<button type="submit" class="btn-addToCart form-catalog__text" style="float:right">
								Добавить в корзину
							</button>
							<input name="${token.name}" type="hidden" value="${token.value}">
						</form>`
						prodview.append(postWrp);
						$('#spinner-border').hide();
					}

				}
			}
		});
	}
	ev.preventDefault();
});

$('.catalogForm-sub').on('click', '.form__clear', function (ev) {
	//const formClear = document.querySelector(".form__clear");
	$('input:checked').prop('checked', false);
	$('input[type=number]').each(function () { $(this).val(''); });
	$('#select-category option:first').prop('selected', true);
	$('input[type=text]').each(function () { $(this).val(''); });

	createURL();
});
// function autoCheckPagination() {
// 	const path = document.location.pathname.slice(-1);
// 	let paging = document.getElementById('pagingbuttons');
// 	var btns = paging.getElementsByClassName("page-item");	

// 	for (var i = 0; i < btns.length; i++) {
// 		if (path == i + 1)
// 		{
// 			btns[i].classList.add("active");
// 		}
// 	}
// }
// autoCheckPagination();

$('.catalogForm-sub').on('change click', '.autocomplete__results-item, .form__clear, .autocomplete__clear, input[type=checkbox], [name=category], [name=sortOrder], [name=minPrice], [name=maxPrice]', '', function (ev) {
	$('#spinner-border').show();
	let searchName = document.querySelector(".searchByName");
	let name = $(this).attr('data-value') ?? "";
	searchName.value = name;
	let autocompleteClear = document.querySelector('.autocomplete__clear');
	if (searchName.value != '') {
		autocompleteClear.hidden = false;
		$('input:checked').prop('checked', false);
		$('input[type=number]').each(function () { $(this).val(''); });
		$('#select-category option:first').prop('selected', true);
	}
	else {
		autocompleteClear.hidden = true;
	}
	createURL();
	let path = ev.target.baseURI.split("?"); // забираем путь
	let getUrl = '/catalog/' + 1 + "?" + path[1];
	let url = getUrl + "&handler=Products";
	var token = GetAntiForgeryToken();
	let prodview = document.getElementById('prod-view');
	let response = fetch(url)
		.then((response) => response.json())
		.then(data => {
			let pagingJson = JSON.parse(data.pagingInfoJson)
			window.history.pushState({}, '', getUrl); // устанавливаем URL в строку браузера
			let paging = document.getElementById('pagingbuttons');
			const page = document.location.pathname.slice(-1);
			$('#pagingbuttons').empty();

			pagingButtons = document.createElement("li");
			pagingButtons.classList.add("page-item");
			if (pagingJson.CurrentPage <= 1) {
				pagingButtons.innerHTML = `<button form="catalogForm" class="pagination-btn page-link" type="submit" value="1" formaction="/1">Назад</button>`
			}
			else {
				pagingButtons.innerHTML = `<button form="catalogForm" class="pagination-btn page-link" type="submit" value="${pagingJson.CurrentPage - 1}" formaction="/${pagingJson.CurrentPage - 1}">Назад</button>`
			}
			paging.appendChild(pagingButtons);
			if (pagingJson.CurrentPage <= 1) {
				pagingButtons.classList.add("disabled");
			}
			for (let i = 1; i <= pagingJson.TotalPages; i++) {
				pagingButtons = document.createElement("li");
				pagingButtons.classList.add("page-item");
				pagingButtons.innerHTML = `<button form="catalogForm" class="pagination-btn page-link" type="submit" value="${i}" formaction="/${i}">${i}</button>`
				paging.appendChild(pagingButtons);
				if (pagingJson.CurrentPage == page && i == page) {
					pagingButtons.classList.add("active");
				}
			}
			pagingButtons = document.createElement("li");
			pagingButtons.classList.add("page-item");
			pagingButtons.innerHTML = `<button form="catalogForm" class="pagination-btn page-link" type="submit" value="${pagingJson.CurrentPage + 1}" formaction="/${pagingJson.CurrentPage + 1}">Вперёд</button>`
			paging.appendChild(pagingButtons);
			if (pagingJson.CurrentPage >= pagingJson.TotalPages) {
				pagingButtons.classList.add("disabled");
			}

			let placeholderMinPrice = document.getElementById('minPrice')
			placeholderMinPrice.placeholder = pagingJson.PlaceholderMinPrice;
			let placeholderMaxPrice = document.getElementById('maxPrice')
			placeholderMaxPrice.placeholder = pagingJson.PlaceholderMaxPrice;

			let productJson = JSON.parse(data.json);
			let postWrp;
			if (productJson.length == 0) {
				$('#prod-view').empty();
				postWrp = document.createElement("div");
				postWrp.classList.add("col-12");
				postWrp.innerHTML = `<div class="" id="">
				<p class="">Нет товаров</p></div>`
				prodview.append(postWrp);
				$('#spinner-border').hide();
			}
			if (productJson.length > 0) {
				$('#prod-view').empty();
				for (let i = 0; i < productJson.length; i++) {
					let price = productJson[i].Price;
					let oldPrice = productJson[i].OldPrice;
					postWrp = document.createElement("div");
					postWrp.classList.add("search-card__wrapper");
					postWrp.innerHTML = `<a class="search-card__info" href="/catalog/${productJson[i].Category.CategoryName}/${productJson[i].Name}/${productJson[i].ProductID}">
						<div id="img-container">
    						<div class="search-card__image-wrapper">
    							<img class="search-card__image" src="${productJson[i].MainPhoto}">
 							</div>
  						</div>
  						<div class="footer-card__wrapper">
							<p class="footer-card__text">${productJson[i].Name}</p>
							<p class="footer-card__price">${price.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' })}</p>
    					</div>
					</a>
					<form id="ProductID=${productJson[i].ProductID}" class="addToCart" method="post" action="/Cart" >
						<input type="hidden" name="ProductID" id="pr_ProductID" value="${productJson[i].ProductID}" />
						<input type="hidden" name="returnUrl" value="${document.location.href}" />
						<button type="submit" class="btn-addToCart form-catalog__text" style="float:right">
							Добавить в корзину
						</button>
						<input name="${token.name}" type="hidden" value="${token.value}">
					</form>`
					prodview.append(postWrp);
					$('#spinner-border').hide();
				}
			}
		})
});


$('.searchByName').on('input', function (ev) {
	let searchName = document.querySelector(".searchByName").value
	let getUrl = '/catalog/?'// забираем путь
	let url = getUrl + "handler=AllProducts";
	let autocompleteResults = document.getElementById('autocomplete__results');
	let autocomplete;
	let autocompleteClear = document.querySelector('.autocomplete__clear');
	let response = fetch(url)
		.then((response) => response.json())
		.then(data => {
			let productJson = JSON.parse(data.json);
			if (productJson.length > 0) {
				$('#autocomplete__results').empty();
				let namesArr = [];
				let elementIs = true;
				for (let i = 0; i < productJson.length; i++) {
					let productName = productJson[i].Name.toUpperCase();
					let productArtikul = productJson[i].Artikul.toUpperCase();
					if (productName.startsWith(searchName.toUpperCase())) {
						namesArr.push(productName);
					}
					if (productArtikul.startsWith(searchName.toUpperCase())) {
						namesArr.push(productArtikul);
					}
				}
				if (searchName.length > 0) {
					autocompleteClear.hidden = false;
				}
				else {
					autocompleteClear.hidden = true;
				}
				if (elementIs === true && searchName.length > 0) {
					if (namesArr.length > 0) {
						for (let i = 0; i < namesArr.length; i++) {
							autocomplete = document.createElement("li");
							autocomplete.classList.add("autocomplete__results-item");
							autocomplete.setAttribute("data-value", namesArr[i])
							autocomplete.innerHTML = `${namesArr[i]}`
							autocompleteResults.append(autocomplete);
							autocompleteResults.hidden = false;
						};
					}
					else {
						autocomplete = document.createElement("li");
						autocomplete.classList.add("autocomplete__results-item");
						autocomplete.innerHTML = `Нет результатов.`
						autocompleteResults.append(autocomplete);
						autocompleteResults.hidden = false;
					}
				}
				$(document).on('click', ".autocomplete__results-item, .autocomplete__clear", function (e) {
					namesArr.splice(0, namesArr.length);
					elementIs = false;
					autocompleteResults.hidden = true;
				})
			}
		})
});


function GetAntiForgeryToken() {
	var tokenField = $("input[type='hidden'][name$='RequestVerificationToken']");
	if (tokenField.length == 0) {
		return null;
	} else {
		return {
			name: tokenField[0].name,
			value: tokenField[0].value
		};
	}
}

// $(function () {
// 	var header = $(".header");
// 	let scroll = $(window).scrollTop();
// 	if (scroll > 1) {
// 		header.addClass("scrolled");
// 	} else {
// 		header.removeClass("scrolled");
// 	}
// 	$(window).scroll(function () {
// 		scroll = $(window).scrollTop();

// 		if (scroll > 1) {
// 			header.addClass("scrolled");
// 		}
// 		else {
// 			header.removeClass("scrolled");
// 		}
// 	});
// });

jQuery(document).ready(function () {
	let check;
	$('#menu-checkbox').on('click', function () {
		if (check == false) {
			$('body').css('overflow', '');
			check = true;
		}
		else {
			$('body').css('overflow', 'hidden');
			check = false;
		}
	});
});

$('.open-input').click(function () {
	var target = $(this).data('target');
	$("#input" + target).toggle();
	document.getElementById('plus' + target).classList.toggle("icon-plus-active");
});

jQuery(document).ready(function () {
	$('.variable-width').slick({
		dots: true,
		arrows: false,
		infinite: true,
		speed: 300,
		slidesToShow: 1,
		centerMode: true,
		variableWidth: true
	});

	$('.similar-product').slick({
		lazyLoad: 'ondemand',
		dots: true,
		arrows: false,
		infinite: false,
		swipe: true,
		speed: 300,
		slidesToShow: 4,
		slidesToScroll: 4,
		responsive: [
			{
				breakpoint: 1025,
				settings: {
					slidesToShow: 3,
					slidesToScroll: 3,
					infinite: true,
					dots: true
				}
			},
			{
				breakpoint: 800,
				settings: {
					slidesToShow: 2,
					slidesToScroll: 2
				}
			},
			{
				breakpoint: 500,
				settings: {
					slidesToShow: 1,
					slidesToScroll: 1
				}
			}
		]
	});
});

$(document).ready(function () {
	$('.minus').click(function () {
		var $input = $(this).parent().find('input');
		var count = parseInt($input.val()) - 1;
		count = count < 1 ? 1 : count;
		$input.val(count);
		$input.change();
		return false;
	});
	$('.plus').click(function () {
		var $input = $(this).parent().find('input');
		$input.val(parseInt($input.val()) + 1);
		$input.change();
		return false;
	});
});

$('.number').on("change click", ".quantity-count, .minus, .plus", function (ev) {
	let priceElement = document.getElementById('product-price');
	let priceProduct = priceElement.getAttribute('price-value');
	let quantity = document.getElementById('quantity-count').value;
	let totalPrice = priceProduct * quantity;
	$('#total-price').html(totalPrice.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' }));
});

$('.recalculation').on("change click", ".quantity-product-count", function (ev) {
	$(this).closest('.recalculation').submit();
});

$('#checkout-form').on("change click", "#tel", function () {
	document.getElementById('tel').addEventListener('input', function (e) {
		var x = e.target.value.replace(/\D/g, '').match(/(\d{0,1})(\d{0,3})(\d{0,3})(\d{0,4})/);
		e.target.value = '+' + (x[1] = 7) + ' (' + x[2] + ') ' + x[3] + '-' + x[4]
	});
});


$(document).ready(function () {
	var windowWidth = window.innerWidth;
	// Set the threshold for mobile devices (e.g. 767px)
	var mobileThreshold = 767;
	// Check if the window width is below the mobile threshold
	if (windowWidth < mobileThreshold) {
		// Disable ScrollMagic on mobile devices
		var controller = new ScrollMagic.Controller();
		controller.enabled(false);
	} else {
		// Initialize ScrollMagic on desktop devices
		var ctrl = new ScrollMagic.Controller();
		// Your ScrollMagic code here...
		$(".scroll-m").each(function (i) {
			let target = $(this).find(".hook");
			var tl = new TimelineMax();
			tl.from(target, 1, { opacity: 0.4, y: 105 })
				.to(target, 1, { opacity: 0.4 });

			new ScrollMagic.Scene({
				triggerElement: this,
				duration: "67%", //40
				triggerHook: 0.55 //0.85
			})
				.setTween(tl)
				.addTo(ctrl)
			// .addIndicators({
			// 	colorTrigger: "black",
			// 	colorStart: "black",
			// 	colorEnd: "black",
			// 	indent: 40
			// });
		});
	}
});


$(document).ready(function () {
	var ctrl = new ScrollMagic.Controller();
	// Your ScrollMagic code here...
	$(".header-animation").each(function (i) {
		let target = $(this).find(".header");
		let targetLogo = $(this).find(".logo");
		let targetBurger = $(this).find(".burger-line");
		let targetBtn = $(this).find(".a-btn__header");
		let targetBtnAfter = CSSRulePlugin.getRule(".a-btn__header:after");
		let targetCart = $(this).find(".cart-btn");
		var tl = new TimelineMax();
		tl.to(target, 1, { height: 75, backgroundColor: "#000000" });
		tl.to(targetLogo, 0.5, { color: "#ffffff", fontSize: "1.5rem" });
		tl.to(targetCart, 0.5, { color: "#ffffff", fill: "#ffffff" });
		tl.to(targetBurger, 0.5, { backgroundColor: "#ffffff" });
		tl.to(targetBtn, 0.5, { color: "#ffffff", fontSize: "0.75rem" });
		tl.to(targetBtnAfter, 0.5, { background: "#ffffff" });
		new ScrollMagic.Scene({
			triggerElement: this,
			duration: "30%", //40
			triggerHook: 0 //0.85
		})
			.setTween(tl)
			.addTo(ctrl)
		// .addIndicators({
		// 	colorTrigger: "black",
		// 	colorStart: "black",
		// 	colorEnd: "black",
		// 	indent: 40
		// });
	});
});