/* 
 * jQuery - Selected Text Sharer - Plugin v1.5
 * http://www.aakashweb.com/
 * Copyright 2010, Aakash Chakravarthy
 * Released under the MIT License.
 */
 
(function($) {
    $.fn.selectedTextSharer= function(options) {
        
        var defaults = {
			title : 'Share',
            lists : 'Google,http://www.google.com/search?q=%s',
			truncateChars : 115,
			extraClass : '',
			borderColor : '#444',
			background : '#fff',
			titleColor : '#f2f2f2',
			hoverColor : '#ffffcc',
			textColor : '#000'
        };
        
        var options = $.extend(defaults, options);
        
        var listSplit = [];
        var lstSplit = [];
        
		function getBaseUrl( url ) {
			if (url.indexOf('.') == -1 || url.indexOf('/') == -1){ 
				return false; 
			}
			var result = url.substr(0, url.indexOf( '/' , url.indexOf('.') ) + 1 );
			return result;
		}
		
        function splitList(){
			listSplit = (options.lists).split('|');
            for(i=0; i<listSplit.length; i++){
                lstSplit[i] = listSplit[i].split(',');
            }
        }    
        
        function createListBox(ele, e){
			e = e || window.event;
			stsBox = '<div class="stsBox ' + options.extraClass + '"><div class="title">' + options.title + '<a href="http://www.aakashweb.com" title="What is this ?" target="_blank" rel="follow">?</a></div><div class="list"><ul></ul></div><span class="arrow"></span></div>';
			
			if(ele.height() == $('body').height()){
				ele.append(stsBox);
			}else{
				ele.after(stsBox);
			}
        }
		
		function addToList(ele){
			
			if(ele.height() == $('body').height()){
				stsBoxEle = ele.find('.stsBox');
			}else{
				stsBoxEle = ele.next('.stsBox');
			}
			
			for(i=0; i<listSplit.length; i++){
				if(lstSplit[i][0] != null){
					if(lstSplit[i][2] != null){
						iconUrl = lstSplit[i][2].split(' ').join('');
						if(iconUrl == 'favicon'){
							img = '<img src="' + getBaseUrl(lstSplit[i][1]) + 'favicon.ico" width="16" height="16" alt="' + lstSplit[i][0] + '"/> ';
						}else{
							img = '<img src="' + lstSplit[i][2] + '" width="16" height="16" alt="' + lstSplit[i][0] + '"/> ';
						}
						
					}else{
						img = '';
					}
					tempList= '<li>' + img + '<a rel="' + lstSplit[i][1] + '" title="' + lstSplit[i][0] + '" href="#">' + lstSplit[i][0] + '</a></li>';
					stsBoxEle.find('ul').append(tempList);
				}
			}
		}
       
		function applyCss(ele){
			
			if(ele.height() == $('body').height()){
				stsBoxEle = ele.find('.stsBox');
			}else{
				stsBoxEle = ele.next('.stsBox');
			}
			
			stsBoxEle.css({
				position: 'absolute',
				display: 'none',
				margin: 0,
				width: '200px',
				'-webkit-border-radius': '5px',
				'-moz-border-radius': '5px',
				'border-radius': '5px',
				border: '5px solid ' + options.borderColor
			});
			
			stsBoxEle.find('img').css({
				'vertical-align': 'middle'
			});
			
			stsBoxEle.find('.title').css({
				color: options.textColor,
				background: options.titleColor,
				padding: 3,
				'border-bottom': '1px solid #e5e5e5'
			});
			
			stsBoxEle.find('.title').find('a').css({
				float: 'right',
				'padding-left': '5',
				'padding-right': '5'
			});
			
			stsBoxEle.find('a').css({
				color: options.textColor,
				'text-decoration': 'none'
			});
			
			stsBoxEle.find('.list').css({
				background: options.background
			});
			
			$('.stsBox ul, .stsBox li').css({
				'list-style': 'none',
				'margin': 0,
				'padding': 0,
				cursor: 'pointer'
			});
			
			$('.stsBox li').css({
				'padding': 3
			});
			
			stsBoxEle.find('.arrow').css({
				width: 0, height: 0, 'line-height': 0,
				'border-left': '10px solid transparent',
				'border-top': '15px solid ' + options.borderColor,
				position: 'absolute',
				bottom:'-19px'
			});
			
			stsBoxEle.find('li').hover(function(){
				$(this).css({ background: options.hoverColor });
			}, function(){
				$(this).css({ background: 'none' });
			});
		}

		function getSelectionText(){
			if (window.getSelection){
				selectionTxt = window.getSelection();
			}
			else if (document.getSelection){
				selectionTxt = document.getSelection();
			}
			else if (document.selection){
				selectionTxt = document.selection.createRange().text;
			}
			
			return selectionTxt;
		}
		
		String.prototype.trunc = function(n){
			return this.substr(0,n-1) + (this.length > n ? '...' : '');
		};
		
		function init(ele){
			splitList();
			createListBox(ele);
			addToList(ele);
			applyCss(ele);
		}
		
        return this.each(function() {
            
			init($(this));
			
			$(this).mouseup(function(e){
									 
				if ($(e.target).closest('.stsBox').length){
					return;
				}
					
				if(getSelectionText() != ''){
					
					if($(this).height() == $('body').height()){
						stsBoxEle = $(this).find('.stsBox');
					}else{
						stsBoxEle = $(this).next('.stsBox');
					}
					
					var x = e.pageX;
					var y = e.pageY;
					
					stsBoxEle.fadeIn('fast');
					
					stsBoxEle.css({
						top: y - (stsBoxEle.outerHeight() + 30),
						left: x - 30
					});
					
					$('.stsBox li a').each(function(){
						$(this).attr('rev', getSelectionText());
					});
					
				}
				
			});
			
			$('.stsBox li').click(function(){
				sUrl = $(this).children('a').attr('rel');
				selectedText = $(this).children('a').attr('rev');
				theUrl = sUrl.replace('%s', selectedText);
				theUrl = theUrl.replace('%ts', selectedText.trunc(options.truncateChars));
				window.open(theUrl, 'sts_window');
			});
			
			$(document).mousedown(function(e) {
				if ($(e.target).closest('.stsBox').length)
					return;
					
				$('.stsBox').fadeOut('fast');
			});
			
			
			
			
        });
        
    };
        
})(jQuery);   