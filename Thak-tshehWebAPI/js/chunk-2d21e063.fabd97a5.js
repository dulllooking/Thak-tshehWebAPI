(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-2d21e063"],{d496:function(t,e,n){"use strict";n.r(e);var c=n("7a23"),i={class:"loginBannerBG py-10 border-bottom border-dark"},r={class:"container"},d=Object(c["i"])("h2",{class:"text-dark text-center mb-5 fw-light fs-1"},"- 註冊 -",-1),o={class:"row justify-content-center"},a={class:"col-md-6 border border-md-0 border-dark border-widths-2 rounded-6 px-lg-6 px-md-5 px-5 py-md-8 py-5 d-flex flex-column justify-content-center"},s=Object(c["i"])("h4",{class:"fs-4 text-center text-dark mb-3"}," 恭喜您成功開通帳號！ ",-1),l=Object(c["i"])("p",{class:"text-dark text-center mb-32"}," 很棒！您終於加入讀冊伙伴的一員了，趕快登入會員吧！ ",-1),u={class:"row justify-content-center"},b={class:"col-md-8"},p=Object(c["k"])(" 回到登入頁 ");function f(t,e,n,f,j,h){var O=Object(c["E"])("router-link");return Object(c["x"])(),Object(c["h"])("div",i,[Object(c["i"])("div",r,[d,Object(c["i"])("div",o,[Object(c["i"])("div",a,[s,l,Object(c["i"])("div",u,[Object(c["i"])("div",b,[Object(c["l"])(O,{to:"/login",type:"button",class:"btn btn-dark rounded-pill text-white w-100 py-13"},{default:Object(c["M"])((function(){return[p]})),_:1})])])])])])])}n("ac1f"),n("1276");var j={data:function(){return{mailGuid:{Guid:""}}},created:function(){var t=location.href;if(-1!==t.indexOf("?"))for(var e=t.split("?"),n=0;n<=e.length-1;n++)"guid"===e[n].split("=")[0]&&(this.mailGuid.Guid=e[n].split("=")[1]);this.$apiHelper.put("api/users/auth-mail",this.mailGuid).then((function(t){console.log(t.data)})).catch((function(t){console.log("response: ",t.res.data)}))}},h=n("6b0d"),O=n.n(h);const m=O()(j,[["render",f]]);e["default"]=m}}]);
//# sourceMappingURL=chunk-2d21e063.fabd97a5.js.map