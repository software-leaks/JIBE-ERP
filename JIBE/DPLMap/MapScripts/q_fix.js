

reset_level_on_new_lang = 1;
use_xml_lang = 1;
modify_elements = new Array('q', 'blockquote');
default_language = 'en';

function get_quotes(lang) {
	var quotes = new Array(4)
	
	switch (lang) {
	case 'en': quotes[0] = '\u2018'; quotes[1] = '\u201c';
			   quotes[2] = '\u2019'; quotes[3] = '\u201d';
			   break;
			   
	case 'en-us': quotes[0] = '\u2019'; quotes[1] = '\u201d';
				  quotes[2] = '\u2018'; quotes[3] = '\u201c';
				  break;
				  
	case 'da': quotes[0] = '\u201e'; quotes[1] = '\u201a';
			   quotes[2] = '\u201d'; quotes[3] = '\u2019';
			   break;

	case 'fr': quotes[0] = '\u00ab'; quotes[1] = '\u2039';
		   quotes[2] = '\u00bb'; quotes[3] = '\u203a';
		   break;
		   
	case 'de': quotes[0] = '\u201e'; quotes[1] = '\u201a';
			   quotes[2] = '\u201c'; quotes[3] = '\u2018';
			   break;
			   
	case 'no': quotes[0] = '\u201e'; quotes[1] = '\u201a';
			   quotes[2] = '\u201d'; quotes[3] = '\u2019';
			   break;
			   
	case 'se': quotes[0] = '\u201d'; quotes[1] = '\u2019';
			   quotes[2] = '\u201d'; quotes[3] = '\u2019';
			   break;
			   
	default: return get_quotes('en');
	}
	
	return quotes;
}

function parse_element(elem, lang, qlevel) {
	var lang_ = lang;
	var qlevel_ = qlevel;
	
	if (use_xml_lang && elem.attributes != null && elem.attributes.getNamedItem("xml:lang") != null) {
		var xmllang = elem.attributes.getNamedItem("xml:lang");
		if (xmllang.nodeValue != lang_ && xmllang.nodeValue != '')
			lang_ = xmllang.nodeValue;
		else if (elem.lang != lang_ && elem.nodeName != '#text' && elem.lang != '')
			lang_ = elem.lang;
		if (reset_level_on_new_lang)
			qlevel_ = 0;
	} else if (elem.lang != lang_ && elem.nodeName != '#text' && elem.lang != '') { // carry on language info
		lang_ = elem.lang;
		if (reset_level_on_new_lang)
			qlevel_ = 0;
	}

	
	var nodename_ = elem.nodeName.toLowerCase();
	
	for (var i = 0; i < modify_elements.length; ++i) {
		if (modify_elements[i] == nodename_) {
			var qts = get_quotes(lang_);

			if (nodename_ == 'q')
				elem.innerHTML = qts[0 + qlevel_ % 2] + elem.innerHTML + qts[2 + qlevel_ % 2];
			else if (nodename_ == 'blockquote') {
				if (elem.firstChild == null)
					elem.innerHTML = qts[0 + qlevel_ % 2] + elem.innerHTML;
				else if (elem.firstChild.nodeName == '#text')
					elem.firstChild.nodeValue = qts[0 + qlevel_ % 2] + elem.firstChild.nodeValue;
				else {
					if (elem.firstChild.canHaveHTML)
						elem.firstChild.innerHTML = qts[0 + qlevel_ % 2] + elem.firstChild.innerHTML;
					else
						elem.insertAdjacentText('afterBegin', qts[0 + qlevel_ % 2]);
				}
					
				if (elem.lastChild == null)
					elem.innerHTML = elem.innerHTML + qts[2 + qlevel_ % 2];
				else if (elem.lastChild.nodeName == '#text')
					elem.lastChild.nodeValue = elem.lastChild.nodeValue + qts[2 + qlevel_ % 2];
				else {
					if (elem.lastChild.canHaveHTML)
						elem.lastChild.innerHTML = elem.lastChild.innerHTML + qts[2 + qlevel_ % 2];
					else
						elem.insertAdjacentText('beforeEnd', qts[2 + qlevel_ % 2]);
				}
			}
			
			++qlevel_;
			break;
		}
	}
	
	if (elem.childNodes == null)
		return;
		
	for (var i = 0; i < elem.childNodes.length; ++i)
		parse_element(elem.childNodes(i), lang_, qlevel_);
}

function q_fix() {
	if (navigator.appName == 'Microsoft Internet Explorer'
		&& navigator.appVersion.indexOf('MSIE 6.0') != -1) {
		
		var body_ = document.body;
		if (body_ == null)
			return;
			
		var html = document.getElementsByTagName("HTML");
		if (html.length != 1)
			return;
		html = html[0];

		var lang_ = default_language;
		if (use_xml_lang) {
			var xmllang = html.attributes.getNamedItem("xml:lang");
			if (xmllang != null && xmllang.nodeValue != lang_ && xmllang.nodeValue != '')
				lang_ = xmllang.nodeValue;
			else if (html.lang != null && html.lang != '')
				lang_ = html.lang;
		} else if (html.lang != null && html.lang != '') {
			lang_ = html.lang;
		}
		
		if (use_xml_lang && body_.attributes != null && body_.attributes.getNamedItem("xml:lang") != null) {
			var xmllang = body_.attributes.getNamedItem("xml:lang");
			if (xmllang != null && xmllang.nodeValue != lang_ && xmllang.nodeValue != '')
				lang_ = xmllang.nodeValue;
			else if (body_.lang != null && body_.lang != '')
				lang_ = body_.lang;
		} else if (body_.lang != null && body_.lang != '') {
			lang_ = body_.lang;
		}
			
		parse_element(body_, lang_, 0);
	}
}