(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-2d0db2c4"],{"6f75":function(e,t,r){"use strict";r.r(t);var s=r("7a23"),o={class:"bg-secondary py-10"},a={class:"container"},l=Object(s["i"])("h2",{class:"text-dark text-center mb-5 display-6 fw-bold"},"- 登入 -",-1),n={class:"row justify-content-center"},i={class:"col-lg-8 col-md-10 border border-md-0 border-dark border-widths-2 rounded-6 px-lg-6 px-md-5 px-5 py-md-8 py-5"},c={class:"row flex-md-row flex-column align-items-center"},d={class:"col pe-0"},b={class:"pe-md-7 border-0 border-end border-dark"},p={class:"mb-3"},u=Object(s["i"])("label",{for:"loginUsername",class:"form-label fs-4 text-dark"},"電子信箱",-1),f={class:"mb-2"},m=Object(s["i"])("label",{for:"loginPassword",class:"form-label fs-4 text-dark"},"密碼",-1),j={class:"input-group"},x=Object(s["i"])("span",{class:"material-icons text-dark position-absolute top-50 end-0 translate-middle-y pe-3","data-bs-container":"body","data-bs-toggle":"popover","data-bs-placement":"top","data-bs-content":"Top popover"},"help_outline",-1),O={class:"mb-5 d-flex justify-content-between align-items-center"},g=Object(s["i"])("div",{class:"form-check"},[Object(s["i"])("input",{type:"checkbox",class:"form-check-input rounded-0 boredr-dark",id:"rememberMe"}),Object(s["i"])("label",{class:"form-check-label fs-8",for:"rememberMe"}," 記住我的帳號 ")],-1),k=Object(s["k"])(" 忘記密碼 "),h=Object(s["i"])("span",{class:"material-icons fs-6 me-1"},"error",-1),w={class:"fs-7 mb-0"},v=Object(s["i"])("button",{type:"submit",class:"btn btn-dark w-100 text-white rounded-pill py-13"}," 登入 ",-1),y={class:"fs-7 mt-2"},M=Object(s["k"])(" 還不是會員嗎？快來 "),T=Object(s["k"])(" 免費註冊會員 "),E=Object(s["k"])(" 吧！ "),J=Object(s["j"])('<div class="col mt-md-0 mt-5"><div class="row justify-content-md-end justify-content-center ps-md-7"><ul class="list-unstyled flex-column g-0"><li class="rounded-pill bg-transparent border border-dark mx-auto mb-4 px-xxl-7 px-4"><a href="#" class="loginMethod text-dark fw-loght btn d-flex align-items-center py-13"><i class="fab fa-facebook-square fs-4 pe-md-32 pe-3"></i> 使用 Facebook 登入</a></li><li class="rounded-pill bg-transparent border border-dark mx-auto mb-4 px-xxl-7 px-4"><a href="#" class="loginMethod text-dark fw-loght btn d-flex align-items-center py-13"><i class="fab fa-google fs-4 pe-md-32 pe-3"></i> 使用 Google 登入</a></li><li class="rounded-pill bg-transparent border border-dark mx-auto px-xxl-7 px-4"><a href="#" class="loginMethod text-dark fw-loght btn d-flex align-items-center py-13"><i class="fab fa-line fs-4 pe-md-32 pe-3"></i> 使用 Line 登入</a></li></ul></div></div>',1);function P(e,t,r,P,S,U){var I=Object(s["E"])("router-link");return Object(s["x"])(),Object(s["h"])("div",o,[Object(s["i"])("div",a,[l,Object(s["i"])("div",n,[Object(s["i"])("div",i,[Object(s["i"])("div",c,[Object(s["i"])("div",d,[Object(s["i"])("div",b,[Object(s["i"])("form",{onSubmit:t[2]||(t[2]=Object(s["O"])((function(){return U.logIn&&U.logIn.apply(U,arguments)}),["prevent"]))},[Object(s["i"])("div",p,[u,Object(s["N"])(Object(s["i"])("input",{type:"email",id:"loginUsername",class:"form-control-lightYellow rounded-pill position-relative ps-3 border-0 py-13",placeholder:"thaktsheh@email.com",required:"",autofocus:"","onUpdate:modelValue":t[0]||(t[0]=function(e){return S.user.Account=e})},null,512),[[s["J"],S.user.Account]])]),Object(s["i"])("div",f,[m,Object(s["i"])("div",j,[Object(s["N"])(Object(s["i"])("input",{type:"password",id:"loginPassword",class:"form-control-lightYellow rounded-pill position-relative ps-3 border-0 py-13",placeholder:"Password",required:"","onUpdate:modelValue":t[1]||(t[1]=function(e){return S.user.Password=e})},null,512),[[s["J"],S.user.Password]]),x])]),Object(s["i"])("div",O,[g,Object(s["l"])(I,{to:"/forget-password",class:"text-decoration-underline text-dark fs-8"},{default:Object(s["M"])((function(){return[k]})),_:1})]),Object(s["i"])("div",{class:Object(s["r"])(["errorMessenger text-danger d-flex mb-2 justify-content-start align-items-center",{"d-none":!S.isError}])},[h,Object(s["i"])("p",w,Object(s["H"])(S.ErrorText),1)],2),v],32),Object(s["i"])("p",y,[M,Object(s["l"])(I,{to:"/sign-up",class:"linkforSignUp undoMember text-decoration-underline"},{default:Object(s["M"])((function(){return[T]})),_:1}),E])])]),J])])])])])}var S={data:function(){return{user:{Account:"",Password:""},userTokenData:{Status:"",JwtToken:""},isError:!1,ErrorText:""}},methods:{logIn:function(){var e=this;this.$apiHelper.post("api/users/login",this.user).then((function(t){if(t.data.Status){e.userTokenData=t.data;var r=t.data.JwtToken;localStorage.setItem("JwtToken",r),location.href="/"}else e.isError=!0,e.ErrorText=t.data.Message}))}}},U=r("6b0d"),I=r.n(U);const q=I()(S,[["render",P]]);t["default"]=q}}]);
//# sourceMappingURL=chunk-2d0db2c4.2c71033c.js.map