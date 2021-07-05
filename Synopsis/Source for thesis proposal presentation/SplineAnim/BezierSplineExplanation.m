close all force;
s = 4.45;
P1 = ([15, 1] + 3)*s;
P2 = ([1,82] + 3)*s;
P3 = ([120,161] + 3)*s;
P4 = ([192, 107] + 3)*s;

back = ones(750,900,3) * 255;
f = figure;  
splineX = [];
splineY = [];

crsr = 0;
for f = 0:0.003:1.002
    %clf; 
    P12 = midP(P1,P2, f);
    P23 = midP(P2, P3, f);
    P34 = midP(P3, P4, f);
    Pa = midP(P12,P23, f);
    Pb = midP(P23,P34, f);
    Ps = midP(Pa, Pb, f);
    splineX(end+1) = Ps(1);
    splineY(end+1) = Ps(2);
    
    if (f == 0.999)        
        f = 1;
        imshow(back); hold on;
        plot(splineX, splineY, 'Color', [0,174/255,239/255], 'LineWidth', 8);
        plot([P1(1), P2(1), P3(1), P4(1)], [P1(2), P2(2), P3(2), P4(2)], 'LineWidth', 2);
        plot([P12(1), P23(1), P34(1)], [P12(2), P23(2), P34(2)], 'LineWidth', 2);
        plot([Pa(1), Pb(1)], [Pa(2), Pb(2)], 'LineWidth', 2);
        plot([P12(1), P23(1), P34(1)], [P12(2), P23(2), P34(2)], 'x', 'MarkerSize', 20);    
        plot([Pa(1), Pb(1)], [Pa(2), Pb(2)], 'x', 'MarkerSize', 20);
        plot(Ps(1), Ps(2), 'x', 'MarkerSize', 20);    


        text(650,50,['f = ', num2str(f)], 'FontSize', 30);
        %annotation('textarrow',[10,Ps(1)],[10, Ps(2)],'String',['f = ', num2str(f)]);

        %set(gca, 'units','pixels','position',[0 0 1920 1080]);
        %p = get(gca, 'position');
        frame_h = get(handle(gcf),'JavaFrame');
        set(frame_h,'Maximized',1);
        F  = getframe;
        num = num2str(crsr);
        while(length(num) <3)
            num = ['0', num];
        end
            imwrite(F.cdata, ['d:\imgs\img', num,'.jpg']);
    end
    crsr = crsr + 1;
end
