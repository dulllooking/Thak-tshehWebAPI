(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-2d0d75de"],{7708:function(t,e,s){"use strict";s.r(e);var o=s("7a23"),r={class:"pt-10"},n={class:"loginBannerBG py-10"},a={class:"container"},c=Object(o["i"])("h2",{class:"text-dark text-center mb-5 fw-light fs-1"},"- 驗證舊密碼 -",-1),i={class:"row justify-content-center"},d={class:"col-md-6"},l={class:"border border-dark border-widths-2 rounded-6 px-6 py-7"},u=Object(o["i"])("div",{class:"d-flex align-items-end mb-3"},[Object(o["i"])("h3",{class:"text-dark fs-4 fw-normal me-3 mb-0"},"現在的密碼"),Object(o["i"])("p",{class:"fs-7 text-dark fw-light m-0"}," ＊請先輸入目前登入之密碼，以便重置新密碼 ")],-1),b={class:"row"},p={class:"col-md-9"},w=Object(o["i"])("div",{class:"col-md-3"},[Object(o["i"])("button",{class:"btn btn-dark rounded-pill w-100 text-white py-13",type:"submit",id:"button-addon2"}," 確認送出 ")],-1);function h(t,e,s,h,O,f){return Object(o["x"])(),Object(o["h"])("div",r,[Object(o["i"])("div",n,[Object(o["i"])("div",a,[c,Object(o["i"])("div",i,[Object(o["i"])("div",d,[Object(o["i"])("div",l,[u,Object(o["i"])("form",{onSubmit:e[1]||(e[1]=Object(o["O"])((function(){return f.authPassword&&f.authPassword.apply(f,arguments)}),["prevent"]))},[Object(o["i"])("div",b,[Object(o["i"])("div",p,[Object(o["N"])(Object(o["i"])("input",{type:"password",class:"rounded-pill position-relative ps-3 border-0 form-control-lightYellow py-13",id:"inputOldPassword",placeholder:"","onUpdate:modelValue":e[0]||(e[0]=function(t){return O.user.Password=t})},null,512),[[o["J"],O.user.Password]]),Object(o["i"])("span",{class:Object(o["r"])(["text-danger fs-8 mt-2",{"d-none":O.isError}])}," 密碼錯誤 ",2)]),w])],32)])])])])])])}var O={data:function(){return{user:{Account:"",Password:""},userTokenData:{JwtToken:""},isError:!0}},created:function(){var t=localStorage.getItem("JwtToken");console.log(t),t||this.$router.push("/login")},methods:{authPassword:function(){var t=this;console.log("authPassword"),console.log(this.user.Password),this.$apiHelper.post("api/users/auth-password",this.user).then((function(e){console.log(e);var s=e.data.JwtToken;t.userTokenData.JwtToken=s,localStorage.setItem("JwtToken",s);var o=e.status;if(console.log(o),200===o)return t.$router.push("/reset-password")})).catch((function(e){console.log("response: ",e.res.data),t.isError=!t.isError,console.log(t.isError)}))}}},f=s("6b0d"),j=s.n(f);const m=j()(O,[["render",h]]);e["default"]=m}}]);
//# sourceMappingURL=chunk-2d0d75de.0886cf2d.js.map